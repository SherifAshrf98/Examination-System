using Examination.Application.Common;
using Examination.Application.Interfaces;
using Examination.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Services
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly JwtSettings _jwtSettings;

		public TokenService(UserManager<AppUser> userManager, JwtSettings jwtSettings)
		{
			_userManager = userManager;
			_jwtSettings = jwtSettings;
		}

		public async Task<string> GenerateTokenAsync(AppUser user)
		{
			var claims = new List<Claim>
			{
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim(JwtRegisteredClaimNames.NameId, user.Id),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			};

			var roles = await _userManager.GetRolesAsync(user);

			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
