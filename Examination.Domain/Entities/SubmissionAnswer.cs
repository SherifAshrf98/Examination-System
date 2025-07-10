using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
	public class SubmissionAnswer : BaseEntity
	{
		public int SubmissionId { get; set; }
		public int QuestionId { get; set; }
		public int SelectedOptionId { get; set; }
		public ExamSubmission Submission { get; set; }
		public Question Question { get; set; }
		public QuestionOption SelectedOption { get; set; }
	}
}
