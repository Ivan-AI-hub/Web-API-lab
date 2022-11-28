using MediatR;

namespace Whosales.Application.Comands.ProductTypes
{
	public record DeleteProductType(int id) : IRequest;
}
