using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.ProductTypes.Handlers
{
	internal class GetProductTypesCountHandler : BaseGetHandler<GetProductTypesCount, int>
	{
		public GetProductTypesCountHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<int> Handle(GetProductTypesCount request, CancellationToken cancellationToken)
		{
			var whereRule = request.whereRule;
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.ProductTypes.Include(x => x.Products).Where(whereRule).Count());
		}
	}
}
