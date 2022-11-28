using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Products
{
	public record GetProductsCount(Func<Product, bool>? whereRule = null) : IRequest<int>;
}
