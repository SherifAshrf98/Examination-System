using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using Examination.Application.Dtos.Student;
using Examination.Application.Dtos.Subject;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities.Enums;
using Examination.Domain.Entities.Identity;
using Examination.Infrastructure.Data;
using Examination.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Examination.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<bool>> ChangeStudentState(string id, ChangeUserStateDto changeUserStateDto)
		{
			var existingUser = await _userManager.FindByIdAsync(id);

			if (existingUser == null)
				return Result<bool>.NotFound("User Not Found");

			existingUser.Status = changeUserStateDto.Status;

			await _unitOfWork.CompleteAsync();

			return Result<bool>.Success(true);
		}

		public async Task<Result<Pagination<StudentDto>>> GetAllStudentsAsync(int pageNumber, int pageSize)
		{
			var paginated = await _unitOfWork.UserRepository.GetAllStudentsAsync(pageNumber, pageSize);

			if (paginated == null)
			{
				return Result<Pagination<StudentDto>>.Failure("No Students Found");
			}

			return Result<Pagination<StudentDto>>.Success(paginated);
		}
		public async Task<Result<StudentDto>> GetStudentByIdAsync(string id)
		{
			var student = await _userManager.FindByIdAsync(id);

			if (student is null)
			{
				return Result<StudentDto>.Failure("Student not found");
			}
			var studentDto = new StudentDto
			{
				Id = student.Id,
				UserName = student.UserName,
				Email = student.Email,
				FullName = $"{student.FirstName} {student.LastName}",
				status = student.Status.ToString()
			};

			return Result<StudentDto>.Success(studentDto);
		}
		public async Task<Result<StudentDto>> GetStudentByUserNameAsync(string userName)
		{
			var student = await _userManager.FindByNameAsync(userName);

			if (student is null)
			{
				return Result<StudentDto>.Failure("Student not found");
			}
			var studentDto = new StudentDto
			{
				Id = student.Id,
				UserName = student.UserName,
				Email = student.Email,
				FullName = $"{student.FirstName} {student.LastName}",
				status = student.Status.ToString()
			};

			return Result<StudentDto>.Success(studentDto);
		}
		public async Task<Result<StudentWithSubjectsDto>> GetStudentWithSubjects(string id)
		{
			var studentSubjects = await _unitOfWork.UserRepository.GetStudentWithSubjects(id);

			if (studentSubjects is null)
			{
				return Result<StudentWithSubjectsDto>.NotFound("Student not found");
			}

			return Result<StudentWithSubjectsDto>.Success(studentSubjects);
		}
		public async Task<Result<int>> GetTotalStudentsCountAsync()
		{
			var studntRole = await _roleManager.FindByNameAsync("Student");

			if (studntRole is null)
			{
				return Result<int>.Failure("No Students Found");
			}

			var studentsCount = await _unitOfWork.UserRepository.GetStudentsCountAsync(studntRole.Id);

			return Result<int>.Success(studentsCount);
		}
	}
}
