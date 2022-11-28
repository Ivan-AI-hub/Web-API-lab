using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Employers.Handlers
{
	internal class GetEmployersQueryPageHandler : BaseGetHandler<GetEmployersQueryPage, IQueryable<Employer>>
	{
		public GetEmployersQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Employer>> Handle(GetEmployersQueryPage request, CancellationToken cancellationToken)
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

			return Task.FromResult(_context.Employers
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
