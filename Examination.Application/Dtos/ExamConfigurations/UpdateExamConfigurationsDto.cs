namespace Examination.Application.Dtos.ExamConfigurations
{
	public class UpdateExamConfigurationsDto
	{
		public int? NumEasy { get; set; }
		public int? NumMedium { get; set; }
		public int? NumHard { get; set; }
		public int? Duration { get; set; }
	}
}