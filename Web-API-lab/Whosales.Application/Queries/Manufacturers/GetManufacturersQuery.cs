using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Manufacturers
{
	public record GetManufacturersQuery : IRequest<IQueryable<Manufacturer>>;
}
