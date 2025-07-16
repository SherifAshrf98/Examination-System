using Examination.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Dtos.Student
{
	public class ChangeUserStateDto
	{
		public UserStatus Status { get; set; }
	}
}
