using Whosales.Domain;

namespace Whosales.Web.Models.Manufacturers
{
	public record ManufacturersPageViewModel(IEnumerable<Manufacturer> Manufacturers, int PageCount, int CurrentPage, string SortRule,
		string nameTemplate);
}
