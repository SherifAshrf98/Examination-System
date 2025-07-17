using Examination.Application.Common;
using Examination.Application.Dtos.ExamConfigurations;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Services
{
	public class ExamConfigurationsService : IExamConfigurationsService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ExamConfigurationsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Result<int>> AddExamConfigurations(int subjectid, CreateExamConfigurationsDto CreateExamConfigurationsDto)
		{
			var existingSubject = await _unitOfWork.SubjectsRepository.IsExistingAsync(s => s.Id == subjectid);

			if (!existingSubject)
				return Result<int>.NotFound("Subject not found");

			var examConfig = new ExamConfigurations
			{
				SubjectId = subjectid,
				Duration = CreateExamConfigurationsDto.Duration,
				NumEasy = CreateExamConfigurationsDto.NumEasy,
				NumHard = CreateExamConfigurationsDto.NumHard,
				NumMedium = CreateExamConfigurationsDto.NumMedium,
			};

			await _unitOfWork.ExamConfigurationsRepository.AddAsync(examConfig);

			await _unitOfWork.CompleteAsync();

			return Result<int>.Success(examConfig.SubjectId);
		}

		public async Task<Result<ExamConfigurationsDto>> GetConfigurationsBySubjectIdAsync(int subjectId)
		{
			var examConfigs = await _unitOfWork.ExamConfigurationsRepository
			.FirstOrDefaultAsync(c => c.SubjectId == subjectId);

			if (examConfigs is null)
				return Result<ExamConfigurationsDto>.NotFound("Exam Config Was Not Found");

			var examConfigDto = new ExamConfigurationsDto
			{
				Duration = examConfigs.Duration,
				NumEasy = examConfigs.NumEasy,
				NumMedium = examConfigs.NumMedium,
				NumHard = examConfigs.NumHard,
			};

			return Result<ExamConfigurationsDto>.Success(examConfigDto);
		}

		public async Task<Result<bool>> UpdateExamConfigurations(int subjectId, UpdateExamConfigurationsDto updateExamConfigurationsDto)
		{
			var subjectConfig = await _unitOfWork.ExamConfigurationsRepository.FirstOrDefaultAsync(ec => ec.SubjectId == subjectId);

			if (subjectConfig == null)
				return Result<bool>.NotFound("No Configs For This Subject Yet");

			if (updateExamConfigurationsDto.Duration is not null)
				subjectConfig.Duration = updateExamConfigurationsDto.Duration.Value;

			if (updateExamConfigurationsDto.NumEasy is not null)
				subjectConfig.NumEasy = updateExamConfigurationsDto.NumEasy.Value;

			if (updateExamConfigurationsDto.NumMedium is not null)
				subjectConfig.NumMedium = updateExamConfigurationsDto.NumMedium.Value;

			if (updateExamConfigurationsDto.NumHard is not null)
				subjectConfig.NumHard = updateExamConfigurationsDto.NumHard.Value;

			await _unitOfWork.CompleteAsync();

			return Result<bool>.Success(true);
		}

		public async Task<Result<ExamConfigurationsDto>> GetExamConfigurationsByIdAsync(int id)
		{
			var examConfig = await _unitOfWork.ExamConfigurationsRepository.GetByIdAsync(id);

			if (examConfig is null)
				return Result<ExamConfigurationsDto>.NotFound("Exam Config Not Found");

			var examConfigDto = new ExamConfigurationsDto
			{
				Duration = examConfig.Duration,
				NumEasy = examConfig.NumEasy,
				NumMedium = examConfig.NumMedium,
				NumHard = examConfig.NumHard,
			};

			return Result<ExamConfigurationsDto>.Success(examConfigDto);
		}
	}
}
