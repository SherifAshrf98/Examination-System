using Examination.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface ITokenService
	{
		Task<string> GenerateTokenAsync(AppUser user);
	}
}
	