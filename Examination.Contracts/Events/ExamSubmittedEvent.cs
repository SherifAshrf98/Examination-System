using Examination.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Contracts.Events
{
	public class ExamSubmittedEvent
	{
		public int ExamId { get; set; }
		public string StudentId { get; set; }
		public List<QuestionSubmissionDto> Answers { get; set; }
		public List<QuestionCorrectAnswerDto> CorrectAnswers { get; set; }
	}
}
