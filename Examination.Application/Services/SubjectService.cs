using Examination.Application.Common;
using Examination.Application.Dtos.Subject;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;

namespace Examination.Application.Services
{
	public class SubjectService : ISubjectService
	{
		private readonly IUnitOfWork _unitOfWork;

		public SubjectService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<Pagination<SubjectDto>>> GetAllSubjectsAsync(int pageNumber, int pageSize)
		{
			var paginagtion = await _unitOfWork.SubjectsRepository.GetAllSubjectsAsync(pageNumber, pageSize);

			return Result<Pagination<SubjectDto>>.Success(paginagtion);
		}
		public async Task<Result<SubjectDto>> GetSubjectByIdAsync(int id)
		{
			var Subject = await _unitOfWork.SubjectsRepository.GetByIdAsync(id);

			if (Subject == null)
			{
				return Result<SubjectDto>.Failure("Subject not found.");
			}

			var subjectDto = new SubjectDto
			{
				id = Subject.Id,
				Name = Subject.Name
			};

			return Result<SubjectDto>.Success(subjectDto);
		}
		public async Task<Result<bool>> AddSubjectAsync(CreateSubjectDto createSubjectDto)
		{
			var ExistingSubject = await _unitOfWork.SubjectsRepository.GetSubjectByNameAsync(createSubjectDto.Name);

			if (ExistingSubject is not null)
				return Result<bool>.Failure("Subject already exists.");

			var newSubject = new Subject()
			{
				Name = createSubjectDto.Name
			};

			await _unitOfWork.SubjectsRepository.AddAsync(newSubject);

			await _unitOfWork.CompleteAsync();

			return Result<bool>.Success(true);
		}
		public async Task<Result<bool>> UpdateSubjectAsync(int id, UpdateSubjectDto updateSubjectDto)
		{
			var existingSubject = await _unitOfWork.SubjectsRepository.GetByIdAsync(id);

			if (existingSubject == null)
			{
				return Result<bool>.NotFound("Subject not found.");
			}

			if (updateSubjectDto.Name is not null)
			{
				existingSubject.Name = updateSubjectDto.Name;
			}

			await _unitOfWork.CompleteAsync();

			return Result<bool>.Success(true);
		}
		public async Task<Result<bool>> DeleteSubjectAsync(int id)
		{
			var existingSubject = await _unitOfWork.SubjectsRepository.GetByIdAsync(id);

			if (existingSubject == null)
			{
				return Result<bool>.NotFound("Subject not found.");
			}

			_unitOfWork.SubjectsRepository.Delete(existingSubject);

			await _unitOfWork.CompleteAsync();

			return Result<bool>.Success(true);
		}
	}
}