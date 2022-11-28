using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Products
{
	public record GetProductsQuery() : IRequest<IQueryable<Product>>;
}
