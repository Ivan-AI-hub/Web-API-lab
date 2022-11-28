using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Manufacturers
{
	public record GetManufacturersQueryPage(int pageSize, int pageNumber,
		Func<Manufacturer, dynamic>? orderByRule = null, Func<Manufacturer, bool>? whereRule = null) : IRequest<IQueryable<Manufacturer>>;
}
