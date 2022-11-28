using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Manufacturers.Handlers
{
	internal class AddManufacturerHandler : IRequestHandler<AddManufacturer>
	{
		IWholesaleContext _context;
		public AddManufacturerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddManufacturer request, CancellationToken cancellationToken)
		{
			var validator = new ManufacturerValidator();
			var result = validator.Validate(request.Manufacturer);

			if (result.IsValid)
			{
				_context.Manufacturers.Add(request.Manufacturer);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
