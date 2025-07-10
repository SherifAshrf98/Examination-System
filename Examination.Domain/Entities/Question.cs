using Examination.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
	public class Question : BaseEntity
	{
		public int SubjectId { get; set; }
		public string Text { get; set; }
		public DifficultyLevel Difficulty { get; set; }
		public Subject Subject { get; set; }
		public ICollection<QuestionOption> Options { get; set; }
		public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
		public ICollection<SubmissionAnswer> SubmissionAnswers { get; set; } = new List<SubmissionAnswer>();
	}
}
