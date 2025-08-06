namespace Examination.Application.Dtos.Exam
{
	public class ExamResultDto
	{
		public string QuestionText { get; set; }
		public List<ExamResultOptionDto> Options { get; set; }
		public int? SelectedOptionId { get; set; }
		public bool? IsAnswerCorrect { get; set; }
	}
}