using Examination.Domain.Entities;
using Examination.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}

		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<QuestionOption> QuestionOptions { get; set; }
		public DbSet<Exam> Exams { get; set; }
		public DbSet<ExamQuestion> ExamQuestions { get; set; }
		public DbSet<ExamSubmission> Submissions { get; set; }
		public DbSet<SubmissionAnswer> SubmissionAnswers { get; set; }
	}
}

