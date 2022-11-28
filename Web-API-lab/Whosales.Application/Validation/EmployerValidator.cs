using FluentValidation;
using Whosales.Domain;

namespace Whosales.Application.Validation
{
	internal class EmployerValidator : AbstractValidator<Employer>
	{
		public EmployerValidator()
		{
			RuleFor(c => c.Name)
			.Must(c => c.All(Char.IsLetter));
		}
	}
}
