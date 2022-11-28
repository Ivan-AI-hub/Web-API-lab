using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.ReleaseReports.Handlers
{
	internal class GetReleaseReportsCountHandler : BaseGetHandler<GetReleaseReportsCount, int>
	{
		public GetReleaseReportsCountHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<int> Handle(GetReleaseReportsCount request, CancellationToken cancellationToken)
		{
			var whereRule = request.whereRule;
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.ReleaseReports
				.Include(x => x.Product)
				.Include(x => x.Storage)
				.Include(x => x.Customer)
				.Include(x => x.Employer)
				.Where(whereRule).Count());
		}
	}
}
