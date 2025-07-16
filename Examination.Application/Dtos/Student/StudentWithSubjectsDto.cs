using Examination.Application.Dtos.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Dtos.AppUser
{
	public class StudentWithSubjectsDto
	{
		public List<SubjectDto> Subjects { get; set; }
	}
}
