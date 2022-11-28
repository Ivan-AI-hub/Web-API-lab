using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.Manufacturers.Handlers
{
	internal class DeleteManufacturerHandler : IRequestHandler<DeleteManufacturer>
	{
		IWholesaleContext _context;
		public DeleteManufacturerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteManufacturer request, CancellationToken cancellationToken)
		{
			var manufacturer = _context.Manufacturers.FirstOrDefault(x => x.ManufacturerId == request.id);
			if (manufacturer != null)
			{
				_context.Manufacturers.Remove(manufacturer);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
