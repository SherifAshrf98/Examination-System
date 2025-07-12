using Examination.Domain.Entities;
using Examination.Domain.Interfaces;
using Examination.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class ExamsRepository : GenericRepository<Exam>, IExamRepository
	{
		private readonly AppDbContext _dbContext;

		public ExamsRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public Task<IEnumerable<ExamSubmission>> GetAllSubmittedExamsAsync()
		{
			throw new NotImplementedException();
		}
	}
}
