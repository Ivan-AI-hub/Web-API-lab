using FluentValidation;
using Whosales.Domain;

namespace Whosales.Application.Validation
{
	internal class ManufacturerValidator : AbstractValidator<Manufacturer>
	{
		public ManufacturerValidator()
		{
			RuleFor(c => c.Name)
			.Must(c => c.All(Char.IsLetter));
		}
	}
}
