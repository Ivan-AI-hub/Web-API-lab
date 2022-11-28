using MediatR;

namespace Whosales.Application.Comands.Manufacturers
{
	public record DeleteManufacturer(int id) : IRequest;
}
