using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.ProductTypes.Handlers
{
	internal class UpdateProductTypeHandler : IRequestHandler<UpdateProductType>
	{
		IWholesaleContext _context;
		public UpdateProductTypeHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateProductType request, CancellationToken cancellationToken)
		{
			var validator = new ProductTypeValidator();
			var result = validator.Validate(request.ProductType);

			if (result.IsValid)
			{
				request.ProductType.ProductTypeId = request.id;
				_context.ProductTypes.Update(request.ProductType);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
