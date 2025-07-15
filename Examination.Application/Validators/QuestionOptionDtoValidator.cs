using Examination.Application.Dtos.Question;
using Examination.Application.Dtos.QuestionOption;
using FluentValidation;
using FluentValidation.Validators;

namespace Examination.Application.Validators
{
	public class QuestionOptionDtoValidator : AbstractValidator<QuestionOptionDto>
	{
		public QuestionOptionDtoValidator()
		{

			RuleFor(x => x.Text)
				.NotEmpty().WithMessage("Option text is required.");

			RuleFor(x => x.IsCorrect)
				.NotNull().WithMessage("IsCorrect must be provided.");

		}

	}
}