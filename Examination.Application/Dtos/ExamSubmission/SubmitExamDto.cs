using Examination.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Dtos.ExamSubmission
{
	public class SubmitExamDto
	{
		public int ExamId { get; set; }
		public List<SubmissionAnswerDto> Answers { get; set; }	
	}
}
