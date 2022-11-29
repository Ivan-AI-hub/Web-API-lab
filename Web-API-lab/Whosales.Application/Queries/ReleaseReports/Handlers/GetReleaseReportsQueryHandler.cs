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
				.AsQueryable());
		}
	}
}
