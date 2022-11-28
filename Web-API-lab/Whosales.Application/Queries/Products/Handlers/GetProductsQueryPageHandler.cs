using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Products.Handlers
{
	internal class GetProductsQueryPageHandler : BaseGetHandler<GetProductsQueryPage, IQueryable<Product>>
	{
		public GetProductsQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Product>> Handle(GetProductsQueryPage request, CancellationToken cancellationToken)
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

			return Task.FromResult(
				_context.Products
				.Include(x => x.Manufacturer)
				.Include(x => x.Type)
				.Where(whereRule)
				.OrderBy(orderByRule)
				.Skip(request.pageSize * (request.pageNumber - 1))
				.Take(request.pageSize)
				.AsQueryable());
		}
	}
}
