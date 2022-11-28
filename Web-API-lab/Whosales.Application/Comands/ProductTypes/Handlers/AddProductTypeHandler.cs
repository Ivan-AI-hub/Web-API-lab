using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.ProductTypes.Handlers
{
	internal class AddProductTypeHandler : IRequestHandler<AddProductType>
	{
		IWholesaleContext _context;
		public AddProductTypeHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddProductType request, CancellationToken cancellationToken)
		{
			var validator = new ProductTypeValidator();
			var result = validator.Validate(request.ProductType);

			if (result.IsValid)
			{
				_context.ProductTypes.Add(request.ProductType);
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
