using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface INotificationService
	{
		Task NotifyStudentAsync(string studentId, string message);
		Task NotifyAdminAsync(string message);
	}
}
