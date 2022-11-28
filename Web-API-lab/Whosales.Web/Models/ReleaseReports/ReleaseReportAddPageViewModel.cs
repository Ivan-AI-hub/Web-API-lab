using Whosales.Domain;

namespace Whosales.Web.Models.ReleaseReports
{
	public record ReleaseReportAddPageViewModel(
		IEnumerable<Employer> Employers,
		IEnumerable<Product> Products,
		IEnumerable<Customer> Customers,
		IEnumerable<Storage> Storages,
		int selectedEmployerId,
		int selectedProductId,
		int selectedCustomerId,
		int selectedStorageId);
}
