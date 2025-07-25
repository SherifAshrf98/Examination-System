﻿using Examination.Application.Dtos.QuestionOption;

namespace Examination.Application.Dtos.Question
{
	public class QuestionDto
	{
		public int Id { get; set; }
		public int SubjectId { get; set; }
		public string Text { get; set; }
		public string Difficulty { get; set; }
		public List<QuestionOptionDto> Options { get; set; } = new List<QuestionOptionDto>();
	}
}