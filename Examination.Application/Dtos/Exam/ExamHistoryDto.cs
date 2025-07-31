namespace Examination.Infrastructure.Repositories
{
	public class ExamHistoryDto
	{
		public DateTime StartedAt { get; set; }
		public string SubjectName { get; set; }
		public int Duration { get; set; }
		public string StudentName { get; set; }
		public int Score { get; set; }
	}
}