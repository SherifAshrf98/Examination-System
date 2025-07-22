using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Dtos.Exam
{
	public class ExamQuestionDto
	{
		public int questionId { get; set; }
		public string text { get; set; }
		public List<ExamQuestionOptionDto> Options { get; set; }
	}
}
