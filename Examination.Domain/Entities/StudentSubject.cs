using Examination.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
	public class StudentSubject
	{
		public string StudentId { get; set; }
		public AppUser Student { get; set; }
		public int SubjectId { get; set; }
		public Subject Subject { get; set; }
	}
}
