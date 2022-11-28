using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReleaseReports.Handlers
{
	internal class GetReleaseReportsQueryPageHandler : BaseGetHandler<GetReleaseReportsQueryPage, IQueryable<ReleaseReport>>
	{
		public GetReleaseReportsQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<ReleaseReport>> Handle(GetReleaseReportsQueryPage request, CancellationToken cancellationToken)
		{
			var orderByRule = request.orderByRule;
			var whereRule = request.whereRule;
			if (orderByRule == null)
			{
				orderByRule = x => x.Product;
			}
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.ReleaseReports
				   .Include(x => x.Product)
				   .Include(x => x.Storage)
				   .Include(x => x.Customer)
				   .Include(x => x.Employer)
				   .Where(whereRule)
				   .OrderByDescending(orderByRule)
				   .Skip(request.pageSize * (request.pageNumber - 1))
				   .Take(request.pageSize)
				   .AsQueryable());

		}
	}
}
