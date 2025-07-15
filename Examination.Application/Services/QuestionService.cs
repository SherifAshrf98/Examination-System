using Examination.Application.Common;
using Examination.Application.Dtos.Question;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Services
{
	public class QuestionService : IQuestionService
	{
		private readonly IUnitOfWork _unitOfWork;

		public QuestionService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<QuestionDto>> GetQuestionByIdAsync(int id)
		{
			var question = await _unitOfWork.QuestionsRepository.GetQuestionWithOptions(id);

			if (question == null)
			{
				return Result<QuestionDto>.NotFound("Question Not Found");
			}

			return Result<QuestionDto>.Success(question);
		}

		public async Task<Result<int>> CreateQuestionAsync(CreateQuestionDto createQuestionDto)
		{
			var ExisitingSubject = await _unitOfWork.SubjectsRepository.IsExistingAsync(s => s.Id == createQuestionDto.SubjectId);

			if (!ExisitingSubject)
				return Result<int>.NotFound("Subject Not Found");

			var newQuestion = new Question
			{
				SubjectId = createQuestionDto.SubjectId,

				Text = createQuestionDto.Text,

				Difficulty = createQuestionDto.Difficulty,

				Options = createQuestionDto.Options.Select(o => new QuestionOption
				{
					Text = o.Text,
					IsCorrect = o.IsCorrect

				}).ToList()
			};

			await _unitOfWork.QuestionsRepository.AddAsync(newQuestion);

			await _unitOfWork.CompleteAsync();

			return Result<int>.Success(newQuestion.Id);
		}
	}
}
