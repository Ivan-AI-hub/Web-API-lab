using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.ReleaseReports.Handlers
{
	internal class DeleteReleaseReportHandler : IRequestHandler<DeleteReleaseReport>
	{
		IWholesaleContext _context;
		public DeleteReleaseReportHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteReleaseReport request, CancellationToken cancellationToken)
		{
			var ReleaseReport = _context.ReleaseReports.FirstOrDefault(x => x.ReleaseReportId == request.id);
			if (ReleaseReport != null)
			{
				_context.ReleaseReports.Remove(ReleaseReport);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
