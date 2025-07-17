using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
	public class ExamConfigurations : BaseEntity
	{
		public int SubjectId { get; set; }
		public int NumEasy { get; set; }
		public int NumMedium { get; set; }
		public int NumHard { get; set; }
		public int Duration { get; set; }
		public Subject Subject { get; set; }
	}
}
