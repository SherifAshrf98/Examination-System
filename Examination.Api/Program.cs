using Examination.Domain.Entities.Identity;
using Examination.Infrastructure.Data;
using Examination.Infrastructure.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Examination.Api
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();


			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

			var app = builder.Build();


			var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var logger = services.GetRequiredService<ILogger<Program>>();
			try
			{
				var userManager = services.GetRequiredService<UserManager<AppUser>>();

				var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

				await IdentitySeed.SeedIdentityAsync(userManager, roleManager,logger);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred during seeding Identity Data");
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
