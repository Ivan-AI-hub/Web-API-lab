using Whosales.Domain;

namespace Whosales.Web.Models.Products
{
	public record ProductAddPageViewModel(IEnumerable<Manufacturer> Manufacturers,
										  IEnumerable<ProductType> Types,
										  int selectedManufacturerId);

}
