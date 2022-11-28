using Whosales.Domain;

namespace Whosales.Web.Models.Provaiders
{
	public record ProvaidersPageViewModel(IEnumerable<Provaider> Provaiders, int PageCount, int CurrentPage, string SortRule,
		string nameTemplate, string addressTemplate);
}
