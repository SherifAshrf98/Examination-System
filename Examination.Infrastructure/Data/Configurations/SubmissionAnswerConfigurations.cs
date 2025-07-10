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
	public class SubmissionAnswerConfiguration : IEntityTypeConfiguration<SubmissionAnswer>
	{
		public void Configure(EntityTypeBuilder<SubmissionAnswer> builder)
		{
			builder.Property(sa => sa.SelectedOptionId)
				   .IsRequired();

			builder.HasOne(sa => sa.Submission)
				   .WithMany(s => s.SubmissionAnswers)
				   .HasForeignKey(sa => sa.SubmissionId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(sa => sa.Question)
				   .WithMany(q => q.SubmissionAnswers)
				   .HasForeignKey(sa => sa.QuestionId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(sa => sa.SelectedOption)
				   .WithMany(qo => qo.SubmissionAnswers)
				   .HasForeignKey(sa => sa.SelectedOptionId)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
