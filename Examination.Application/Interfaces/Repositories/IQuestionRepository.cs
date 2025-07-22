using Examination.Application.Dtos.Question;
using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces.Repositories
{
	public interface IQuestionRepository : IGenericRepository<Question>
	{
		public Task<QuestionDto> GetQuestionWithOptions(int id);
		public Task<IReadOnlyList<Question>> GetRandomQuestions(int subjectId, int numOfEasy, int numOfMedium, int numOfHard);
	}
}
