using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Manufacturers.Handlers
{
	internal class GetManufacturersQueryPageHandler : BaseGetHandler<GetManufacturersQueryPage, IQueryable<Manufacturer>>
	{
		public GetManufacturersQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Manufacturer>> Handle(GetManufacturersQueryPage request, CancellationToken cancellationToken)
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

			return Task.FromResult(_context.Manufacturers
				.Include(x => x.Products)
				.Where(whereRule)
				.OrderByDescending(orderByRule)
				.Skip(request.pageSize * (request.pageNumber - 1))
				.Take(request.pageSize)
				.AsQueryable());
		}
	}
}
