using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Customers.Handlers
{
	internal class GetCustomersQueryPageHandler : BaseGetHandler<GetCustomersQueryPage, IQueryable<Customer>>
	{
		public GetCustomersQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Customer>> Handle(GetCustomersQueryPage request, CancellationToken cancellationToken)
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

			return Task.FromResult(_context.Customers
				   .Include(x => x.ReleaseReports)
				   .Where(whereRule)
				   .OrderByDescending(orderByRule)
				   .Skip(request.pageSize * (request.pageNumber - 1))
				   .Take(request.pageSize)
				   .AsQueryable());

		}
	}
}
