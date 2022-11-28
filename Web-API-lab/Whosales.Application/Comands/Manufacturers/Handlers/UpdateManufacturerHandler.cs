using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Manufacturers.Handlers
{
	internal class UpdateManufacturerHandler : IRequestHandler<UpdateManufacturer>
	{
		IWholesaleContext _context;
		public UpdateManufacturerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateManufacturer request, CancellationToken cancellationToken)
		{
			var validator = new ManufacturerValidator();
			var result = validator.Validate(request.Manufacturer);

			if (result.IsValid)
			{
				request.Manufacturer.ManufacturerId = request.id;
				_context.Manufacturers.Update(request.Manufacturer);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
