using Examination.Application.Common;
using Examination.Application.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Interfaces
{
	public interface IAuthService
	{
		Task<Result<bool>> RegisterAsync(RegisterDto registerDto);
		Task<Result<string>> LoginAsync(LoginDto loginDto);
	}
}
