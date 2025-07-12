using Examination.Application.Dtos;
using Examination.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examination.Api.Controllers
{
	[Route("api/auth/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AccountController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var result = await _authService.RegisterAsync(registerDto);

			if (result.IsSuccess)
				return Ok(new { Message = "Registeration Succeeded !" });

			return BadRequest(new { errors = result.Errors });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var result = await _authService.LoginAsync(loginDto);

			if (result.IsSuccess)
				return Ok(new { Token = result.Value });

			return BadRequest(result.Errors);
		}
	}
}
