using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Products
{
	public record GetProductById(int Id) : IRequest<Product?>;
}
