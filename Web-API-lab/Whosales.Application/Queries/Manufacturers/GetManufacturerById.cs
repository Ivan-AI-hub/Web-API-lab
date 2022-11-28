using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Manufacturers
{
	public record GetManufacturerById(int Id) : IRequest<Manufacturer?>;
}
