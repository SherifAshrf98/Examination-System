﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Dtos.AppUser
{
	public class StudentDto
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string status { get; set; }
	}
}
