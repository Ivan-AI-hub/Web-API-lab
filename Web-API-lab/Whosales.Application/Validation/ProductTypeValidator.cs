using FluentValidation;
using Whosales.Domain;

namespace Whosales.Application.Validation
{
	internal class ProductTypeValidator : AbstractValidator<ProductType>
	{
		public ProductTypeValidator()
		{
			RuleFor(c => c.Name)
				.Must(c => c.Replace(" ", "").All(Char.IsLetter));
		}
	}
}
