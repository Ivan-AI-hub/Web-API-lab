using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Manufacturers.Handlers
{
	internal class GetManufacturerByIdHandler : BaseGetHandler<GetManufacturerById, Manufacturer?>
	{
		public GetManufacturerByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<Manufacturer?> Handle(GetManufacturerById request, CancellationToken cancellationToken)
		{
			var manufacturer = _context.Manufacturers
				.Include(x => x.Products)
				.FirstOrDefault(x => x.ManufacturerId == request.Id);
			return Task.FromResult(manufacturer);
		}
	}
}
