using Whosales.Domain;

namespace Whosales.Web.Models.Products
{
	public record ProductUpdatePageViewModel(Product Product, IEnumerable<Manufacturer> Manufacturers, IEnumerable<ProductType> Types);
}
