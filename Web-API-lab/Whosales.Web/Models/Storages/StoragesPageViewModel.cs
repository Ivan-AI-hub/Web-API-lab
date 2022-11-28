using Whosales.Domain;

namespace Whosales.Web.Models.Storages
{
	public record StoragesPageViewModel(IEnumerable<Storage> Storages, int PageCount, int CurrentPage, string SortRule,
		string nameTemplate);
}
