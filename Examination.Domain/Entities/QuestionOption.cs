using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
	public class QuestionOption : BaseEntity
	{
		public int QuestionID { get; set; }
		public string Text { get; set; }
		public bool IsCorrect { get; set; }
		public Question Question { get; set; }
		public ICollection<SubmissionAnswer> SubmissionAnswers { get; set; }

	}
}
