using FluentValidation;
using Whosales.Domain;

namespace Whosales.Application.Validation
{
	internal class ReceiptReportValidator : AbstractValidator<ReceiptReport>
	{
		public ReceiptReportValidator()
		{
			RuleFor(c => c.Volume)
			.Must(c => c >= 0);
		}
	}
}
