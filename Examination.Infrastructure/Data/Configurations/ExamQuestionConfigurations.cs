using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Data.Configurations
{
	using Examination.Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestion>
	{
		public void Configure(EntityTypeBuilder<ExamQuestion> builder)
		{
			builder.HasKey(eq => new { eq.ExamId, eq.QuestionId });

			builder.HasOne(eq => eq.Exam)
				   .WithMany(e => e.ExamQuestions)
				   .HasForeignKey(eq => eq.ExamId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(eq => eq.Question)
				   .WithMany(q => q.ExamQuestions)
				   .HasForeignKey(eq => eq.QuestionId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
