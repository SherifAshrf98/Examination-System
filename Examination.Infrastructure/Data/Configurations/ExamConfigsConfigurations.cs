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
	public class ExamConfigsConfigurations : IEntityTypeConfiguration<Examination.Domain.Entities.ExamConfigurations>
	{


		public void Configure(EntityTypeBuilder<Domain.Entities.ExamConfigurations> builder)
		{
			builder.HasIndex(ec => ec.SubjectId)
				.IsUnique();
		}
	}
}
