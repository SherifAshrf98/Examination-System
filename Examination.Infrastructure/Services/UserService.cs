using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using Examination.Application.Dtos.Subject;
using Examination.Application.Interfaces;
using Examination.Domain.Entities.Identity;
using Examination.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Examination.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly AppDbContext _dbContext;

		public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_dbContext = dbContext;
		}

		public async Task<Result<Pagination<StudentDto>>> GetAllStudentsAsync(int pageNumber, int pageSize)
		{
			var paginatedList = await (from user in _dbContext.Users
									   join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
									   join role in _dbContext.Roles on userRole.RoleId equals role.Id
									   where role.Name == "Student"
									   select user)
							 .Skip((pageNumber - 1) * pageSize)
							 .Take(pageSize).
							 Select(user => new StudentDto
							 {
								 Id = user.Id,
								 UserName = user.UserName,
								 Email = user.Email,
								 FullName = $"{user.FirstName} {user.LastName}",
								 status = user.Status.ToString()
							 }).ToListAsync();

			return Result<Pagination<StudentDto>>.Success(new Pagination<StudentDto>
			{
				Items = paginatedList,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalCount = await StudentsCountAsync()
			});
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

		public async Task<Result<StudentWithSubjectsDto>> GetStudentWithSubjects(string id)
		{
			var studentSubjects = await _dbContext.Users.Where(u => u.Id == id)
				.Include(u => u.StudentSubjects)
				.ThenInclude(ss => ss.Subject)
				.Select(s => new StudentWithSubjectsDto
				{
					Subjects = s.StudentSubjects.Select(ss => new SubjectDto
					{
						id = ss.Subject.Id,
						Name = ss.Subject.Name,
					}).ToList()
				}).FirstOrDefaultAsync();

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

			var studentsCount = await _dbContext.UserRoles
				.Where(ur => ur.RoleId == studntRole.Id)
				.CountAsync();

			return Result<int>.Success(studentsCount);
		}
		private async Task<int> StudentsCountAsync()
		{
			var studntRole = await _roleManager.FindByNameAsync("Student");

			if (studntRole is null)
			{
				return 0;
			}

			var studentsCount = await _dbContext.UserRoles
				.Where(ur => ur.RoleId == studntRole.Id)
				.CountAsync();
			return studentsCount;
		}
	}
}
