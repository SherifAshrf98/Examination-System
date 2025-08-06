namespace Examination.Application.Dtos.Dashboards
{
	public class StudentDashboardStatsDto
	{
		public int TotalExams { get; set; }
		public int CompletedExams { get; set; }
		public double AverageScore { get; set; }
		public int TotalSubjects { get; set; }
	}
}