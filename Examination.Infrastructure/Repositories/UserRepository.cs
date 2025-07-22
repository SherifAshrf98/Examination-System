using Examination.Application.Common;
using Examination.Application.Dtos.AppUser;
using Examination.Application.Dtos.Subject;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _dbContext;

		public UserRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<Pagination<StudentDto>> GetAllStudentsAsync(int pageNumber, int pageSize)
		{
			var paginatedList = await (from user in _dbContext.Users
									   join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
									   join role in _dbContext.Roles on userRole.RoleId equals role.Id
									   where role.Name == "Student"
									   select user)
								 .Skip((pageNumber - 1) * pageSize)
								 .Take(pageSize)
								 .Select(user => new StudentDto
								 {
									 Id = user.Id,
									 UserName = user.UserName,
									 Email = user.Email,
									 FullName = $"{user.FirstName} {user.LastName}",
									 status = user.Status.ToString()
								 }).ToListAsync();

			var totalCount = await _dbContext.UserRoles
							.Where(ur => ur.RoleId == "00db0e79-2fc4-4882-8cde-e7b07af481fc")
							.CountAsync();
			return new Pagination<StudentDto>
			{
				Items = paginatedList,
				PageNumber = pageNumber,
				PageSize = pageSize,
				PageCount = (int)Math.Ceiling((double)totalCount / pageSize),
				TotalCount = totalCount
			};
		}
		public async Task<int> GetStudentsCountAsync(string roleId)
		{
			var studentsCount = await _dbContext.UserRoles
							.Where(ur => ur.RoleId == roleId)
							.CountAsync();
			return studentsCount;
		}
		public async Task<StudentWithSubjectsDto> GetStudentWithSubjects(string id)
		{
			return await _dbContext.Users.Where(u => u.Id == id)
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
		}
	}
}
