using Whosales.Domain;

namespace Whosales.Web.Models.ProductTypes
{
	public record ProductTypesPageViewModel(IEnumerable<ProductType> ProductTypes, int PageCount, int CurrentPage, string SortRule,
		string nameTemplate, string featureTemplate);
}
