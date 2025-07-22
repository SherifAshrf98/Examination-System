namespace Examination.Application.Dtos
{
	public class DashboardStatsDto
	{
		public int TotalStudents { get; set; }
		public int TotalPassedExams { get; set; }
		public int TotalFailedExams { get; set; }
		public int TotalSubmittedExams { get; set; }
	}
}