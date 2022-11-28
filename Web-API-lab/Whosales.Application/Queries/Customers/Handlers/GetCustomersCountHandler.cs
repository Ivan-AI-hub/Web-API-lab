using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.Customers.Handlers
{
	internal class GetCustomersCountHandler : BaseGetHandler<GetCustomersCount, int>
	{
		public GetCustomersCountHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<int> Handle(GetCustomersCount request, CancellationToken cancellationToken)
		{
			var whereRule = request.whereRule;
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.Customers.Include(x => x.ReleaseReports).Where(whereRule).Count());
		}
	}
}
