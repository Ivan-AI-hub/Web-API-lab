using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ProductTypes
{
	public record GetProductTypesCount(Func<ProductType, bool>? whereRule = null) : IRequest<int>;
}
