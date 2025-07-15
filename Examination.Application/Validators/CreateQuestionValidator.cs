using Examination.Application.Dtos.Question;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Validators
{
	public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
	{
		public CreateQuestionValidator()
		{
			RuleFor(x => x.SubjectId)
		   .GreaterThan(0)
		   .WithMessage("SubjectId must be greater than 0.");

			RuleFor(x => x.Text)
				.NotEmpty()
				.WithMessage("Question text is required.");

			RuleFor(x => x.Difficulty)
		   .IsInEnum().WithMessage("Difficulty must be one of: Easy, Medium, Hard");

			RuleFor(x => x.Options)
				.NotEmpty().WithMessage("At least one option is required.")
				.Must(o => o.Count == 4)
				.WithMessage("Exactly 4 options must be provided.");

			RuleFor(x => x.Options.Count(o => o.IsCorrect))
				.Equal(1)
				.WithMessage("Exactly one option must be marked as correct.");

			RuleForEach(x => x.Options).SetValidator(new QuestionOptionDtoValidator());
		}
	}
}

