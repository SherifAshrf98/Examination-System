using Examination.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Data.Configurations
{
	internal class StudentSubjectConfigurations : IEntityTypeConfiguration<StudentSubject>
	{
		public void Configure(EntityTypeBuilder<StudentSubject> builder)
		{
			builder.HasKey(ss => new { ss.StudentId, ss.SubjectId });

			builder.HasOne(ss => ss.Student)
				   .WithMany(u => u.StudentSubjects)
				   .HasForeignKey(ss => ss.StudentId);

			builder.HasOne(ss => ss.Subject)
				   .WithMany(s => s.StudentSubjects)
				   .HasForeignKey(ss => ss.SubjectId);
		}
	}
}
