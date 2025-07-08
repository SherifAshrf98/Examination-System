using Microsoft.AspNetCore.Identity;


namespace Examination.Domain.Entities.Identity
{
	public class AppUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
		public ICollection<ExamSubmission> Submissions { get; set; } = new HashSet<ExamSubmission>();
	}
}
