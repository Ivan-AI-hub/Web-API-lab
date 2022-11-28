using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Manufacturers
{
	public record UpdateManufacturer(int id, Manufacturer Manufacturer) : IRequest;
}
