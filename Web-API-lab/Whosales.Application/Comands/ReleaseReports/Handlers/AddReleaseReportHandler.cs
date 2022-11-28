using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.ReleaseReports.Handlers
{
	internal class AddReleaseReportHandler : IRequestHandler<AddReleaseReport>
	{
		IWholesaleContext _context;
		public AddReleaseReportHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddReleaseReport request, CancellationToken cancellationToken)
		{
			var validator = new ReleaseReportValidator();
			var result = validator.Validate(request.ReleaseReport);

			if (result.IsValid)
			{
				_context.ReleaseReports.Add(request.ReleaseReport);
				_context.SaveChanges();
				return Task.FromResult(new Unit());
			}
			else
			{
				throw new Exception(result.Errors.Select(x => x.ErrorMessage).Aggregate((x, y) => x + y));
			}
		}
	}
}
