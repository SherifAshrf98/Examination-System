using Examination.Application.Common;
using Examination.Application.Dtos.Exam;
using Examination.Application.Dtos.QuestionOption;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Domain.Entities.Enums;
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

		public ExamService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Result<ExamDto>> CreateOrCountinueExamAsync(int subjectId, string studentId)
		{
			if (string.IsNullOrWhiteSpace(studentId))
			{
				return Result<ExamDto>.Failure("Invalid student ID.");
			}

			var existingExam = await _unitOfWork.ExamsRepository.GetInProgressExamById(studentId, subjectId);

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

		public async Task<Result<ExamDto>> GetExamById(int id)
		{
			var exam = await _unitOfWork.ExamsRepository.GetExamByIdAsync(id);

			if (exam == null)
			{
				return Result<ExamDto>.Failure("Exam not found.");
			}

			return Result<ExamDto>.Success(exam);

		}
	}
}

