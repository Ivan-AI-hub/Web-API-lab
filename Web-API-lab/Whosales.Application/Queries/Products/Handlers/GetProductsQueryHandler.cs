using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Products.Handlers
{
	internal class GetProductsQueryHandler : BaseGetHandler<GetProductsQuery, IQueryable<Product>>
	{
		public GetProductsQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.Products
				.AsQueryable());
		}
	}
}
