using FluentValidation;
using Whosales.Domain;

namespace Whosales.Application.Validation
{
	internal class ProductValidator : AbstractValidator<Product>
	{
		public ProductValidator()
		{

			RuleFor(c => c.Name)
			.Must(c => c.All(Char.IsLetter));

			RuleFor(c => c.Package)
				.Must(c => c.Replace(" ", "").All(Char.IsLetter));

			RuleFor(c => c.StorageConditions)
				.Must(c => c.Replace(" ", "").All(Char.IsLetter));
		}
	}
}
