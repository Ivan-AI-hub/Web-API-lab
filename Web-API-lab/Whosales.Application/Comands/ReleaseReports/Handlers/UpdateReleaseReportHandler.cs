using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.ReleaseReports.Handlers
{
	internal class UpdateReleaseReportHandler : IRequestHandler<UpdateReleaseReport>
	{
		IWholesaleContext _context;
		public UpdateReleaseReportHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateReleaseReport request, CancellationToken cancellationToken)
		{
			var validator = new ReleaseReportValidator();
			var result = validator.Validate(request.ReleaseReport);

			if (result.IsValid)
			{
				request.ReleaseReport.ReleaseReportId = request.id;
				_context.ReleaseReports.Update(request.ReleaseReport);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
