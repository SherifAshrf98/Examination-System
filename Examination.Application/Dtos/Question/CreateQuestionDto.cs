using Examination.Application.Dtos.QuestionOption;
using Examination.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Dtos.Question
{
	public class CreateQuestionDto
	{
		public int SubjectId { get; set; }
		public string Text { get; set; }
		public DifficultyLevel Difficulty { get; set; }
		public List<QuestionOptionDto> Options { get; set; } = new List<QuestionOptionDto>();
	}
}
