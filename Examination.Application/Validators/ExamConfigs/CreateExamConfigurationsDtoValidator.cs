using Examination.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Validators.ExamConfigs
{
	public class CreateExamConfigurationsDtoValidator : AbstractValidator<ExamConfigurations>
	{
		public CreateExamConfigurationsDtoValidator()
		{

			RuleFor(x => x.NumEasy)
				 .GreaterThanOrEqualTo(1).WithMessage("There must be at least 1 easy question.");

			RuleFor(x => x.NumMedium)
				.GreaterThanOrEqualTo(1).WithMessage("There must be at least 1 medium question.");

			RuleFor(x => x.NumHard)
				.GreaterThanOrEqualTo(1).WithMessage("There must be at least 1 hard question.");

			RuleFor(x => x)
				.Must(x => x.NumEasy + x.NumMedium + x.NumHard == 10)
				.WithMessage("The total number of questions must be exactly 10.");

			RuleFor(x => x.Duration)
				.GreaterThan(0).WithMessage("Duration must be greater than 0.");

		}
	}
}
