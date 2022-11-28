using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.Products
{
	public record AddProduct(Product Product) : IRequest;
}
