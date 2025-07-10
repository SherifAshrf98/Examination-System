using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
	public class Subject : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<Question> Questions { get; set; } = new HashSet<Question>();  
		public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
	}
}
