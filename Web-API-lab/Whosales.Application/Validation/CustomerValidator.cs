using FluentValidation;
using Whosales.Domain;

namespace Whosales.Application.Validation
{
	internal class CustomerValidator : AbstractValidator<Customer>
	{
		public CustomerValidator()
		{

			RuleFor(c => c.Name)
			.Must(c => c.Replace(" ", "").All(Char.IsLetter));

			RuleFor(c => c.TelephoneNumber)
			.Must(IsPhoneValid)
			.Length(13);
		}

		private bool IsPhoneValid(string phone)
		{
			return phone.Substring(1).All(c => Char.IsDigit(c));
		}
	}
}
