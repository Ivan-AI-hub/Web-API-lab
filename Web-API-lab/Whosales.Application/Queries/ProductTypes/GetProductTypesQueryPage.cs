using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ProductTypes
{
	public record GetProductTypesQueryPage(int pageSize, int pageNumber,
		Func<ProductType, dynamic>? orderByRule = null, Func<ProductType, bool>? whereRule = null)
		: IRequest<IQueryable<ProductType>>;
}
