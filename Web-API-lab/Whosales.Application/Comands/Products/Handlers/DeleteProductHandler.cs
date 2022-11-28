using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.Products.Handlers
{
	internal class DeleteProductHandler : IRequestHandler<DeleteProduct>
	{
		IWholesaleContext _context;
		public DeleteProductHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteProduct request, CancellationToken cancellationToken)
		{
			var product = _context.Products.FirstOrDefault(x => x.ProductId == request.id);
			if (product != null)
			{
				_context.Products.Remove(product);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
