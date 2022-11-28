using FluentValidation;
using Whosales.Domain;

namespace Whosales.Application.Validation
{
	internal class ReleaseReportValidator : AbstractValidator<ReleaseReport>
	{
		public ReleaseReportValidator()
		{
			RuleFor(c => c.Volume)
			.Must(c => c >= 0);
		}
	}
}
