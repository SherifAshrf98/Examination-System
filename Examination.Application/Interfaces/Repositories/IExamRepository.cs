using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface IExamRepository : IGenericRepository<Exam>
	{
		Task<IEnumerable<ExamSubmission>> GetAllSubmittedExamsAsync();
	}
}
