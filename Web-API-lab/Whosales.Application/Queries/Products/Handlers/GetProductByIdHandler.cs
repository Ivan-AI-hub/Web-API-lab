using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Products.Handlers
{
	internal class GetProductByIdHandler : BaseGetHandler<GetProductById, Product?>
	{
		public GetProductByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<Product?> Handle(GetProductById request, CancellationToken cancellationToken)
		{
			var product = _context.Products
								.Include(x => x.Manufacturer)
								.Include(x => x.Type)
								.FirstOrDefault(x => x.ProductId == request.Id);
			return Task.FromResult(product);
		}
	}
}
