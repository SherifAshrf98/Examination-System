namespace Examination.Application.Dtos.Exam
{
	public class ExamDto
	{
		public int Id { get; set; }
		public DateTime StartedAt { get; set; }
		public string SubjectName { get; set; }
		public int RemainingTime { get; set; }
		public int Duration { get; set; }
		public List<ExamQuestionDto> Questions { get; set; }
	}
}	