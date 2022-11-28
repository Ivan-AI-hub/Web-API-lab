using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.Products.Handlers
{
	internal class GetProductsCountHandler : BaseGetHandler<GetProductsCount, int>
	{
		public GetProductsCountHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<int> Handle(GetProductsCount request, CancellationToken cancellationToken)
		{
			var whereRule = request.whereRule;
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.Products
				.Include(x => x.Manufacturer)
				.Include(x => x.Type)
				.Where(whereRule)
				.Count());
		}
	}
}
