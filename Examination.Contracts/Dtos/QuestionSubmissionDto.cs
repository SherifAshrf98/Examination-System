﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Contracts.Dtos
{
	public class QuestionSubmissionDto
	{
		public int QuestionId { get; set; }
		public int SelectedOptionId { get; set; }
	}
}
