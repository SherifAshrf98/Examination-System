using Examination.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Examination.Infrastructure.Data.Seeding
{
	public static class IdentitySeed
	{
		public static async Task SeedIdentityAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILogger logger)
		{
			if (!await roleManager.RoleExistsAsync("Admin"))
			{
				var role = new IdentityRole("Admin");

				await roleManager.CreateAsync(role);
			}

			if (!await roleManager.RoleExistsAsync("Student"))
			{
				var role = new IdentityRole("Student");

				await roleManager.CreateAsync(role);
			}

			if (await userManager.FindByEmailAsync("sherifashrf6060@gmail.com") == null)
			{
				var AdminUser = new AppUser
				{
					UserName = "Admin_sherif",
					Email = "sherifashrf6060@gmail.com"
				};

				var CreationResult = await userManager.CreateAsync(AdminUser, "CmPunk@98");

				if (!CreationResult.Succeeded)
				{
					foreach (var Error in CreationResult.Errors)
					{
						logger.LogError($"Error creating admin user: {Error.Description}");
					}

					return;
				}

				if (!await userManager.IsInRoleAsync(AdminUser, "Admin"))
				{
					await userManager.AddToRoleAsync(AdminUser, "Admin");
				}
			}

			if (await userManager.FindByEmailAsync("sherifashrf5050@gmail.com") == null)
			{
				var StudentUser = new AppUser
				{
					UserName = "Sherif2026",
					Email = "sherifashrf5050@gmail.com"
				};

				var CreationResult = await userManager.CreateAsync(StudentUser, "Sherif0101516@");

				if (!CreationResult.Succeeded)
				{
					foreach (var Error in CreationResult.Errors)
					{
						logger.LogError($"Error creating admin user: {Error.Description}");
					}
					return;
				}

				if (!await userManager.IsInRoleAsync(StudentUser, "Student"))
				{
					await userManager.AddToRoleAsync(StudentUser, "Student");
				}
			}
		}
	}
}
