using Examination.Application.Common;
using Examination.Application.Dtos;
using Examination.Application.Interfaces;
using Examination.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace Examination.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}
		public async Task<Result<bool>> RegisterAsync(RegisterDto registerDto)
		{
			var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

			if (existingUser is not null)
				return Result<bool>.Failure("This User Already Exists");

			var newUser = new AppUser
			{
				Email = registerDto.Email,
				UserName = registerDto.Username,
				FirstName = registerDto.FirstName,
				LastName = registerDto.LastName,
			};

			var result = await _userManager.CreateAsync(newUser, registerDto.Password);

			if (!result.Succeeded)
				return Result<bool>.Failure(result.Errors.Select(e => e.Description).ToList());

			return Result<bool>.Success(true);
		}

		public async Task<Result<string>> LoginAsync(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email.ToLower());

			if (user is null)
				return Result<string>.Failure("Invalid Email or Password");

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
			if (!result.Succeeded)
				return Result<string>.Failure("Invalid Email or Password");

			var token = await _tokenService.GenerateTokenAsync(user);

			if (string.IsNullOrEmpty(token))
				return Result<string>.Failure("Failed to generate token");

			return Result<string>.Success(token);
		}
	}
}
