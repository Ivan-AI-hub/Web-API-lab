using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Products
{
	public record UpdateProduct(int id, Product Product) : IRequest;
}
