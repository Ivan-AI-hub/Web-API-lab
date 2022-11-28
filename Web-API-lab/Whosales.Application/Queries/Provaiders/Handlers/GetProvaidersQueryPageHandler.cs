using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Provaiders.Handlers
{
	internal class GetProvaidersQueryPageHandler : BaseGetHandler<GetProvaidersQueryPage, IQueryable<Provaider>>
	{
		public GetProvaidersQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Provaider>> Handle(GetProvaidersQueryPage request, CancellationToken cancellationToken)
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

			return Task.FromResult(_context.Provaiders
				   .Include(x => x.ReceiptReports)
				   .Where(whereRule)
				   .OrderBy(orderByRule)
				   .Skip(request.pageSize * (request.pageNumber - 1))
				   .Take(request.pageSize)
				   .AsQueryable());

		}
	}
}
