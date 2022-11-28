using MediatR;

namespace Whosales.Application.Comands.Products
{
	public record DeleteProduct(int id) : IRequest;
}
