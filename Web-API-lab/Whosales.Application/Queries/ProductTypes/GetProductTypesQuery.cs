using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ProductTypes
{
	public record GetProductTypesQuery : IRequest<IQueryable<ProductType>>;
}
