using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReleaseReports.Handlers
{
	internal class GetReleaseReportsQueryHandler : BaseGetHandler<GetReleaseReportsQuery, IQueryable<ReleaseReport>>
	{

		public GetReleaseReportsQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<ReleaseReport>> Handle(GetReleaseReportsQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.ReleaseReports
				.Include(x => x.Product)
				.Include(x => x.Storage)
				.Include(x => x.Customer)
				.Include(x => x.Employer)
				.AsQueryable());
		}
	}
}
