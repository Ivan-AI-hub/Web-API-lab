using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ProductTypes.Handlers
{
	internal class GetProductTypesQueryPageHandler : BaseGetHandler<GetProductTypesQueryPage, IQueryable<ProductType>>
	{
		public GetProductTypesQueryPageHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<ProductType>> Handle(GetProductTypesQueryPage request, CancellationToken cancellationToken)
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

			return Task.FromResult(_context.ProductTypes
				   .Include(x => x.Products)
				   .Where(whereRule)
				   .OrderBy(orderByRule)
				   .Skip(request.pageSize * (request.pageNumber - 1))
				   .Take(request.pageSize)
				   .AsQueryable());
		}
	}
}
