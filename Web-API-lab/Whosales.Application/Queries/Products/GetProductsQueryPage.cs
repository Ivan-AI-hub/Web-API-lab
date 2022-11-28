using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.Products
{
	public record GetProductsQueryPage(int pageSize, int pageNumber,
		Func<Product, dynamic>? orderByRule = null, Func<Product, bool>? whereRule = null) : IRequest<IQueryable<Product>>;
}
