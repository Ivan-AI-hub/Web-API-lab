using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Products.Handlers
{
	internal class UpdateProductHandler : IRequestHandler<UpdateProduct>
	{
		IWholesaleContext _context;
		public UpdateProductHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateProduct request, CancellationToken cancellationToken)
		{
			var validator = new ProductValidator();
			var result = validator.Validate(request.Product);

			if (result.IsValid)
			{
				request.Product.ProductId = request.id;
				_context.Products.Update(request.Product);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
