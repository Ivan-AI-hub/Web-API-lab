using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ProductTypes
{
	public record GetProductTypeById(int id) : IRequest<ProductType?>;
}
