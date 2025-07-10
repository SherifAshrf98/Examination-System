using Examination.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
	public class Exam : BaseEntity
	{
		public DateTime StartedAt { get; set; }
		public int Duration { get; set; }
		public string StudentId{ get; set; }
		public int SubjectId { get; set; }
		public AppUser Student { get; set; }
		public Subject Subject { get; set; }
		public ICollection<ExamQuestion> ExamQuestions { get; set; }	
		public ExamSubmission Submission { get; set; }

	}
}	
