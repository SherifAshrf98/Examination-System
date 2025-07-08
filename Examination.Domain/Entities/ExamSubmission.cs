using Examination.Domain.Entities.Identity;

namespace Examination.Domain.Entities
{
	public class ExamSubmission : BaseEntity
	{
		public int? Score { get; set; }
		public DateTime SubmittedAt { get; set; }
		public int ExamId { get; set; }
		public int StudentId { get; set; }
		public Exam Exam { get; set; }
		public AppUser Student { get; set; }
		public ICollection<SubmissionAnswer> SubmissionAnswers { get; set; }
	}
}