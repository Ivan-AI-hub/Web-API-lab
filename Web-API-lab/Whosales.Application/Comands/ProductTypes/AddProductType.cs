using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.ProductTypes
{
	public record AddProductType(ProductType ProductType) : IRequest;
}
