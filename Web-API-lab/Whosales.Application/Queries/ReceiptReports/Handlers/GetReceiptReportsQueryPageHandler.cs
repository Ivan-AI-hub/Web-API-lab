using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReceiptReports.Handlers
{
	internal class GetReceiptReportsQueryPageHandler : BaseGetHandler<GetReceiptReportsQueryPage, IQueryable<ReceiptReport>>
	{
		public GetReceiptReportsQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<ReceiptReport>> Handle(GetReceiptReportsQueryPage request, CancellationToken cancellationToken)
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

			return Task.FromResult(_context.ReceiptReports
				   .Include(x => x.Product)
				   .Include(x => x.Storage)
				   .Include(x => x.Provaider)
				   .Include(x => x.Employer)
				   .Where(whereRule)
				   .OrderByDescending(orderByRule)
				   .Skip(request.pageSize * (request.pageNumber - 1))
				   .Take(request.pageSize)
				   .AsQueryable());

		}
	}
}
