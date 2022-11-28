
using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ProductTypes.Handlers
{
	internal class GetProductTypeByIdHandler : BaseGetHandler<GetProductTypeById, ProductType?>
	{
		public GetProductTypeByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<ProductType?> Handle(GetProductTypeById request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.ProductTypes.Include(x => x.Products).FirstOrDefault(x => x.ProductTypeId == request.id));
		}
	}
}
