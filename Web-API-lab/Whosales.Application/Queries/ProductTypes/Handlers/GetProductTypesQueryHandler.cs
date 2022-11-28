using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ProductTypes.Handlers
{
	internal class GetProductTypesQueryHandler : BaseGetHandler<GetProductTypesQuery, IQueryable<ProductType>>
	{

		public GetProductTypesQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<ProductType>> Handle(GetProductTypesQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.ProductTypes.Include(x => x.Products).AsQueryable());
		}
	}
}
