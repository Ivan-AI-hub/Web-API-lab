using Whosales.Domain;

namespace Whosales.Web.Models.Employers
{
	public record EmployersPageViewModel(IEnumerable<Employer> Employers, int PageCount, int CurrentPage, string SortRule,
		string nameTemplate);
}
