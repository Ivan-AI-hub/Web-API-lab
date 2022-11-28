using Whosales.Domain;

namespace Whosales.Web.Models.Products
{
	public record ProductsPageViewModel(IEnumerable<Product> Products,
		IEnumerable<Manufacturer> Manufacturers, IEnumerable<ProductType> Types,
		int PageCount, int CurrentPage, string SortRule,
		string nameTemplate, int ManufacturerId, int TypeId);
}
