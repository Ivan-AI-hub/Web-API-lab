using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Storages.Handlers
{
	internal class GetStoragesQueryPageHandler : BaseGetHandler<GetStoragesQueryPage, IQueryable<Storage>>
	{
		public GetStoragesQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Storage>> Handle(GetStoragesQueryPage request, CancellationToken cancellationToken)
		{
			var orderByRule = request.orderByRule;
			var whereRule = request.whereRule;
			if (orderByRule == null)
			{
				orderByRule = x => x.Name;
			}
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.Storages
				   .Include(x => x.ReleaseReports)
				   .Include(x => x.ReceiptReports)
				   .Where(whereRule)
				   .OrderByDescending(orderByRule)
				   .Skip(request.pageSize * (request.pageNumber - 1))
				   .Take(request.pageSize)
				   .AsQueryable());

		}
	}
}
