using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Manufacturers
{
	public record AddManufacturer(Manufacturer Manufacturer) : IRequest;
}
