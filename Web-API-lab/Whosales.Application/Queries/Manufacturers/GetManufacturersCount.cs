using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Manufacturers
{
	public record GetManufacturersCount(Func<Manufacturer, bool>? whereRule = null) : IRequest<int>;
}
