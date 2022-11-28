using Whosales.Domain;

namespace Whosales.Web.Models.Customers
{
	public record CustomersPageViewModel(IEnumerable<Customer> Customers, int PageCount, int CurrentPage, string SortRule,
		string nameTemplate, string addressTemplate);
}
