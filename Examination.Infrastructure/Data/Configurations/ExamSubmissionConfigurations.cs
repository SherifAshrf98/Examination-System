using Examination.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Data.Configurations
{
	public class ExamSubmissionConfiguration : IEntityTypeConfiguration<ExamSubmission>
	{
		public void Configure(EntityTypeBuilder<ExamSubmission> builder)
		{
			builder.HasIndex(s => s.ExamId).IsUnique();

			builder.Property(s => s.SubmittedAt)
				   .HasDefaultValueSql("GETDATE()");

			builder.HasOne(s => s.Exam)
				   .WithOne(e => e.Submission)
				   .HasForeignKey<ExamSubmission>(s => s.ExamId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(s => s.Student)
				   .WithMany(u => u.Submissions)
				   .HasForeignKey(s => s.StudentId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
