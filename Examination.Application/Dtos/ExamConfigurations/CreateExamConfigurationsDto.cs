using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Dtos.ExamConfigurations
{
	public class CreateExamConfigurationsDto
	{
		public int NumEasy { get; set; }
		public int NumMedium { get; set; }
		public int NumHard { get; set; }
		public int Duration { get; set; }
	}
}
