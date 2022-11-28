using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Manufacturers.Handlers
{
	internal class GetManufacturersQueryHandler : BaseGetHandler<GetManufacturersQuery, IQueryable<Manufacturer>>
	{
		public GetManufacturersQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Manufacturer>> Handle(GetManufacturersQuery request, CancellationToken cancellationToken)
		{
			var manufacturers = _context.Manufacturers
				.Include(x => x.Products)
				.AsQueryable();
			return Task.FromResult(manufacturers);
		}
	}
}
