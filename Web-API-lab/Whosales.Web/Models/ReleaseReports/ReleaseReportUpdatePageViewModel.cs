using Whosales.Domain;

namespace Whosales.Web.Models.ReleaseReports
{
	public record ReleaseReportUpdatePageViewModel(ReleaseReport ReleaseReport,
		IEnumerable<Employer> Employers,
		IEnumerable<Product> Products,
		IEnumerable<Customer> Customers,
		IEnumerable<Storage> Storages);
}
