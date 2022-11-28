using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.ProductTypes.Handlers
{
	internal class DeleteProductTypeHandler : IRequestHandler<DeleteProductType>
	{
		IWholesaleContext _context;
		public DeleteProductTypeHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteProductType request, CancellationToken cancellationToken)
		{
			var ProductType = _context.ProductTypes.FirstOrDefault(x => x.ProductTypeId == request.id);
			if (ProductType != null)
			{
				_context.ProductTypes.Remove(ProductType);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
