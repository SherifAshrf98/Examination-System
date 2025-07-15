using Examination.Application.Common;
using Examination.Application.Dtos.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface IQuestionService
	{
		Task<Result<QuestionDto>> GetQuestionByIdAsync(int id);
		Task<Result<int>> CreateQuestionAsync(CreateQuestionDto createQuestionDto);
	}
}
