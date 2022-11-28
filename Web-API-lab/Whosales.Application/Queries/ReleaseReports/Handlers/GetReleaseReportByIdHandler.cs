using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReleaseReports.Handlers
{
	internal class GetReleaseReportByIdHandler : BaseGetHandler<GetReleaseReportById, ReleaseReport?>
	{

		public GetReleaseReportByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<ReleaseReport?> Handle(GetReleaseReportById request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.ReleaseReports
				.Include(x => x.Product)
				.Include(x => x.Storage)
				.Include(x => x.Customer)
				.Include(x => x.Employer)
				.FirstOrDefault(x => x.ReleaseReportId == request.id));
		}
	}
}
