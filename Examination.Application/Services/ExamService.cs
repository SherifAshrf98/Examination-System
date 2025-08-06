using Examination.Application.Common;
using Examination.Application.Dtos.Exam;
using Examination.Application.Dtos.ExamSubmission;
using Examination.Application.Dtos.QuestionOption;
using Examination.Application.Dtos.Subject;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Contracts.Dtos;
using Examination.Contracts.Events;
using Examination.Domain.Entities;
using Examination.Domain.Entities.Enums;
using Examination.Infrastructure.Repositories;
using MassTransit;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Services
{
	public class ExamService : IExamService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly INotificationManger _notificationManger;

		public ExamService(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint, INotificationManger notificationManger)
		{
			_unitOfWork = unitOfWork;
			_publishEndpoint = publishEndpoint;
			_notificationManger = notificationManger;
		}

		public async Task<Result<ExamDto>> CreateOrCountinueExamAsync(int subjectId, string studentId)
		{
			if (string.IsNullOrWhiteSpace(studentId))
			{
				return Result<ExamDto>.Failure("Invalid student ID.");
			}

			var existingExam = await _unitOfWork.ExamsRepository.GetInProgressExam(studentId, subjectId);

			if (existingExam != null)
			{
				var IsExpired = existingExam.StartedAt.AddMinutes(existingExam.Duration) < DateTime.UtcNow;

				if (IsExpired)
				{
					existingExam.status = ExamStatus.Expired;

					await _unitOfWork.CompleteAsync();

					return Result<ExamDto>.Failure("Your previous exam has expired. You can now start a new exam.");

				}

				int remainingTime = (int)(existingExam.StartedAt.AddMinutes(existingExam.Duration) - DateTime.UtcNow).TotalSeconds;

				var examDto = new ExamDto
				{
					Id = existingExam.Id,
					SubjectName = existingExam.Subject.Name,
					Duration = existingExam.Duration,
					StartedAt = existingExam.StartedAt,
					RemainingTime = remainingTime,
					Questions = existingExam.ExamQuestions.Select(eq => new ExamQuestionDto
					{
						questionId = eq.Question.Id,
						text = eq.Question.Text,
						Options = eq.Question.Options.Select(o => new ExamQuestionOptionDto
						{
							Id = o.Id,
							Text = o.Text,
						}).ToList()
					}).ToList()
				};

				return Result<ExamDto>.Success(examDto);
			}

			var configs = await _unitOfWork.ExamConfigurationsRepository.FirstOrDefaultAsync(c => c.SubjectId == subjectId);

			if (configs == null)
			{
				return Result<ExamDto>.Failure("Exam configurations not found for the specified subject.");
			}

			var questions = await _unitOfWork.QuestionsRepository.GetRandomQuestions(subjectId, configs.NumEasy, configs.NumMedium, configs.NumHard);

			if (questions.Count < configs.NumEasy + configs.NumMedium + configs.NumHard)
			{
				return Result<ExamDto>.Failure("Not enough questions available for the exam.");
			}

			var exam = new Exam
			{
				SubjectId = subjectId,
				StudentId = studentId,
				Duration = configs.Duration,
				StartedAt = DateTime.UtcNow,
				status = ExamStatus.InProgress,
				ExamQuestions = questions.Select(q => new ExamQuestion
				{
					QuestionId = q.Id,
				}).ToList()
			};

			await _unitOfWork.ExamsRepository.AddAsync(exam);

			await _unitOfWork.CompleteAsync();

			var newExamDto = await _unitOfWork.ExamsRepository.GetExamByIdAsync(exam.Id);

			return Result<ExamDto>.Success(newExamDto);
		}
		public async Task<Result<bool>> SubmitExam(string studentId, SubmitExamDto submitExamDto)
		{
			if (string.IsNullOrWhiteSpace(studentId))

				return Result<bool>.Failure("Invalid student ID.");

			var exam = await _unitOfWork.ExamsRepository.GetExamWithQuestionsAndAnswers(submitExamDto.ExamId);

			if (exam == null)
			{
				return Result<bool>.NotFound("Exam Doesn't exisit");
			}

			if (exam.StartedAt.AddMinutes(exam.Duration) < DateTime.UtcNow)
			{
				exam.status = ExamStatus.Expired;

				await _unitOfWork.CompleteAsync();

				return Result<bool>.Failure("this Exam has expired");
			}

			if (exam.status == ExamStatus.Submitted)
			{
				return Result<bool>.Failure("This exam has already been submitted");
			}

			var submission = new ExamSubmission
			{
				ExamId = submitExamDto.ExamId,
				StudentId = exam.StudentId,
				SubmittedAt = DateTime.UtcNow,
				SubmissionAnswers = submitExamDto.Answers.Select(a => new SubmissionAnswer
				{
					QuestionId = a.QuestionId,
					SelectedOptionId = a.SelectedOptionId,
				}).ToList()
			};

			await _unitOfWork.ExamSubmissionsRepository.AddAsync(submission);

			var correctAnswers = exam.ExamQuestions.Select(eq => new QuestionCorrectAnswerDto
			{
				QuestionId = eq.QuestionId,
				CorrectOptionId = eq.Question.Options.First(o => o.IsCorrect).Id
			}).ToList();

			var examSubmittedEvent = new ExamSubmittedEvent
			{
				ExamId = exam.Id,
				StudentId = exam.StudentId,
				Answers = submitExamDto.Answers.Select(a => new QuestionSubmissionDto
				{
					QuestionId = a.QuestionId,
					SelectedOptionId = a.SelectedOptionId,
				}).ToList(),
				CorrectAnswers = correctAnswers
			};

			await _publishEndpoint.Publish(examSubmittedEvent);

			exam.status = ExamStatus.Submitted;

			await _unitOfWork.CompleteAsync();

			await _notificationManger.NotifyAdminsAsync($"Student With id: {studentId} has submitted his {exam.Subject.Name} Exam");

			return Result<bool>.Success(true);
		}

		public async Task<Result<Pagination<ExamHistoryDto>>> GetExamHistoryAsync(int pageNumber, int PageSize)
		{
			var paginatedExams = await _unitOfWork.ExamsRepository.GetExamHistory(pageNumber, PageSize);

			return Result<Pagination<ExamHistoryDto>>.Success(paginatedExams);
		}
		public async Task<Result<ExamDto>> GetExamById(int id)
		{
			var exam = await _unitOfWork.ExamsRepository.GetExamByIdAsync(id);

			if (exam == null)
			{
				return Result<ExamDto>.Failure("Exam not found.");
			}

			return Result<ExamDto>.Success(exam);
		}

		public async Task<Result<List<ExamResultDto>>> GetExamQuestionResultAsync(int examId, string studentId)
		{
			if (string.IsNullOrWhiteSpace(studentId))
			{
				return Result<List<ExamResultDto>>.Failure("Invalid student ID.");
			}
			var exam = await _unitOfWork.ExamsRepository.GetExamResult(examId, studentId);

			if (exam == null)
				return Result<List<ExamResultDto>>.NotFound("Exam not found.");

			if (exam.Submission == null)
				return Result<List<ExamResultDto>>.NotFound("Student has not submitted the exam.");

			var submissionAnswers = exam.Submission.SubmissionAnswers;

			var result = exam.ExamQuestions.Select(eq =>
			{
				var question = eq.Question;
				var studentAnswer = submissionAnswers.FirstOrDefault(sa => sa.QuestionId == question.Id);
				var correctOption = question.Options.FirstOrDefault(o => o.IsCorrect);

				return new ExamResultDto
				{
					QuestionText = question.Text,
					Options = question.Options.Select(o => new ExamResultOptionDto
					{
						Id = o.Id,
						Text = o.Text,
						IsCorrect = o.IsCorrect
					}).ToList(),

					SelectedOptionId = studentAnswer?.SelectedOptionId,

					IsAnswerCorrect = studentAnswer?.SelectedOptionId == correctOption?.Id
				};

			}).ToList();

			return Result<List<ExamResultDto>>.Success(result);
		}
	}
}

