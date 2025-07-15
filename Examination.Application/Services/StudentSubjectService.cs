using Examination.Application.Common;
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
	public class StudentSubjectService : IStudentSubjectService
	{
		private readonly IUnitOfWork _unitOfWork;

		public StudentSubjectService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<bool>> EnrollStudentInSubjectAsync(string studentId, int subjectId)
		{
			var Exsisting = await _unitOfWork.StudentSubjectsRepository.IsStudentEnrolledInSubjectAsync(studentId, subjectId);

			if (Exsisting)
			{
				return Result<bool>.Failure("Student is already enrolled in this subject.");
			}

			var studentSubject = new StudentSubject
			{
				StudentId = studentId,
				SubjectId = subjectId
			};

			await _unitOfWork.StudentSubjectsRepository.AddAsync(studentSubject);

			await _unitOfWork.CompleteAsync();

			return Result<bool>.Success(true);
		}

		public async Task<Result<bool>> UnenrollStudentFromSubjectAsync(string studentId, int subjectId)
		{
			var Exsisting = await _unitOfWork.StudentSubjectsRepository.IsStudentEnrolledInSubjectAsync(studentId, subjectId);

			if (!Exsisting)
				return Result<bool>.Failure("Student is not enrolled in this subject.");

			var studentSubject = await _unitOfWork.StudentSubjectsRepository.GetByStudentIdAndSubjectIdAsync(studentId, subjectId);

			if (studentSubject == null)
			{
				return Result<bool>.NotFound("Student subject record not found.");
			}

			_unitOfWork.StudentSubjectsRepository.Delete(studentSubject);

			await _unitOfWork.CompleteAsync();

			return Result<bool>.Success(true);
		}
	}
}
