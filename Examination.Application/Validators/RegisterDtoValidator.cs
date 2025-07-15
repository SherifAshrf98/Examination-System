using Examination.Application.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Validators
{
	public class RegisterDtoValidator : AbstractValidator<RegisterDto>
	{
		public RegisterDtoValidator()
		{
			RuleFor(x => x.Email)
			 .NotEmpty().WithMessage("Email is required.")
			 .EmailAddress().WithMessage("Email is invalid.");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required.")
				.MinimumLength(6).WithMessage("Password must be at least 6 characters.");

			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("First name is required.");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("Last name is required.");
		}
	}
}
