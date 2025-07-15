using Examination.Api.Helpers;
using Examination.Application.Common;
using Examination.Application.Interfaces;
using Examination.Application.Interfaces.Repositories;
using Examination.Application.Services;
using Examination.Application.Validators;
using Examination.Domain.Entities.Identity;
using Examination.Infrastructure.Data;
using Examination.Infrastructure.Data.Seeding;
using Examination.Infrastructure.Repositories;
using Examination.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Examination.Api
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			#region SwaggerServices 

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			#endregion

			#region FluentValidationServices

			builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
			builder.Services.AddFluentValidationAutoValidation();

			#endregion

			#region AddDbContextServices

			builder.Services.AddDbContext<AppDbContext>(options =>
								options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			#endregion

			#region AddIdentityServices

			builder.Services.AddIdentity<AppUser, IdentityRole>()
							.AddEntityFrameworkStores<AppDbContext>()
							.AddDefaultTokenProviders();
			#endregion

			#region AddAuthServices

			builder.Services.AddSingleton(builder.Configuration.GetSection("Jwt").Get<JwtSettings>());

			builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				var JwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
				var key = Encoding.UTF8.GetBytes(JwtSettings.Key);

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = JwtSettings.Issuer,
					ValidAudience = JwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ClockSkew = TimeSpan.Zero
				};
			});

			#endregion

			#region HandelValidationResponse
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = context =>
				{
					var errors = context.ModelState
						.Where(x => x.Value.Errors.Count > 0)
						.SelectMany(x => x.Value.Errors)
						.Select(x => x.ErrorMessage)
						.ToList();


					var errorResponse = new ApiValidationErrorResponse
					{
						Errors = errors
					};

					return new BadRequestObjectResult(errorResponse);
				};
			});
			#endregion

			#region AddApplicationServices

			builder.Services.AddScoped<ITokenService, TokenService>();
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddScoped<IExamRepository, ExamsRepository>();
			builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
			builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<ISubjectService, SubjectService>();
			builder.Services.AddScoped<IStudentSubjectService, StudentSubjectService>();
			builder.Services.AddScoped<IUserService, UserService>();

			#endregion

			var app = builder.Build();

			#region IdentitySeeding

			var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var logger = services.GetRequiredService<ILogger<Program>>();

			try
			{
				var userManager = services.GetRequiredService<UserManager<AppUser>>();

				var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

				await IdentitySeed.SeedIdentityAsync(userManager, roleManager, logger);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred during seeding Identity Data");
			}

			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
