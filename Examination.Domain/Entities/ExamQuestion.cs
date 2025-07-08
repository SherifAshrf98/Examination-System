using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
	public class ExamQuestion
	{
		public int ExamId { get; set; }
		public int QuestionId { get; set; }
		public Exam Exam { get; set; }
		public Question Question { get; set; }
	}
}
