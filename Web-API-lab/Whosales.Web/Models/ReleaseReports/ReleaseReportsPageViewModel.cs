using Whosales.Domain;

namespace Whosales.Web.Models.ReleaseReports
{
	public record ReleaseReportsPageViewModel(IEnumerable<ReleaseReport> ReleaseReports,
		IEnumerable<Employer> Employers,
		IEnumerable<Product> Products,
		IEnumerable<Customer> Customers,
		IEnumerable<Storage> Storages,
		int PageCount, int CurrentPage, string SortRule,
		int ProductId, int StorageId);
}
