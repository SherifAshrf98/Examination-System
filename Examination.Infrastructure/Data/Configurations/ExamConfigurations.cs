using Examination.Domain.Entities;
using Examination.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Data.Configurations
{
	public class ExamConfigurations : IEntityTypeConfiguration<Exam>
	{
		public void Configure(EntityTypeBuilder<Exam> builder)
		{
			builder.Property(e => e.status)
				.HasConversion<string>()
				.HasDefaultValue(ExamStatus.InProgress).IsRequired();


			builder.HasIndex(e => new { e.StudentId, e.SubjectId })
				.IsUnique()
				.HasFilter("[status] = 'InProgress'");
		}
	}
}
