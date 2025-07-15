using Examination.Application.Dtos.Question;
using Examination.Application.Dtos.QuestionOption;
using Examination.Application.Interfaces.Repositories;
using Examination.Domain.Entities;
using Examination.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
	{
		private readonly AppDbContext _context;

		public QuestionRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<QuestionDto> GetQuestionWithOptions(int id)
		{
			return await _context.Questions
				.Where(q => q.Id == id)
				.Select(o => new QuestionDto
				{
					Id = o.Id,
					Text = o.Text,
					Difficulty = o.Difficulty.ToString(),
					SubjectId = o.SubjectId,
					Options = o.Options.Select(opt => new QuestionOptionDto
					{
						Text = opt.Text,
						IsCorrect = opt.IsCorrect
					}).ToList()
				}).FirstOrDefaultAsync();
		}
	}
}