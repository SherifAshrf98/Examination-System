using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Contracts.Events
{
	public class ExamEvaluatedEvent
	{
		public int ExamId { get; set; }
		public string StudentId { get; set; }
		public int Score { get; set; }
	}
}
