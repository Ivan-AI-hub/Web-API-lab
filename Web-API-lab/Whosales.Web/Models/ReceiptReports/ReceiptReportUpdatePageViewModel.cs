using Whosales.Domain;

namespace Whosales.Web.Models.ReceiptReports
{
	public record ReceiptReportUpdatePageViewModel(ReceiptReport ReceiptReport,
		IEnumerable<Employer> Employers,
		IEnumerable<Product> Products,
		IEnumerable<Provaider> Provaiders,
		IEnumerable<Storage> Storages);
}
