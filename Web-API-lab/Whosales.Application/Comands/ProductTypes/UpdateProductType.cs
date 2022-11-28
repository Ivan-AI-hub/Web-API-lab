using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.ProductTypes
{
	public record UpdateProductType(int id, ProductType ProductType) : IRequest;
}
