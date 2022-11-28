using FluentValidation;
using Whosales.Domain;

namespace Whosales.Application.Validation
{
	internal class StorageValidator : AbstractValidator<Storage>
	{
		public StorageValidator()
		{
			RuleFor(c => c.Name)
			.Must(c => c.All(Char.IsLetter));
		}
	}
}
