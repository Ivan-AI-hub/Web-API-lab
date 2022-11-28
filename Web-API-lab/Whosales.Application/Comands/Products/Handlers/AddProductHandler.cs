using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Products.Handlers
{
	internal class AddProductHandler : IRequestHandler<AddProduct>
	{
		IWholesaleContext _context;
		public AddProductHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddProduct request, CancellationToken cancellationToken)
		{
			var validator = new ProductValidator();
			var result = validator.Validate(request.Product);

			if (result.IsValid)
			{
				_context.Products.Add(request.Product);
				_context.SaveChanges();
				return Task.FromResult(new Unit());
			}
			else
			{
				throw new Exception(result.Errors.Select(x => x.ErrorMessage).Aggregate((x, y) => x + y));
			}
		}
	}
}
