using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Customers.Handlers
{
	internal class GetCustomersQueryHandler : BaseGetHandler<GetCustomersQuery, IQueryable<Customer>>
	{

		public GetCustomersQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.Customers.Include(x => x.ReleaseReports).AsQueryable());
		}
	}
}
