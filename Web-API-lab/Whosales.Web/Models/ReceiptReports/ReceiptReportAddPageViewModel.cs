using Whosales.Domain;

namespace Whosales.Web.Models.ReceiptReports
{
	public record ReceiptReportAddPageViewModel(IEnumerable<Employer> Employers,
		IEnumerable<Product> Products,
		IEnumerable<Provaider> Provaiders,
		IEnumerable<Storage> Storages,
		int selectedEmployerId,
		int selectedProductId,
		int selectedProvaiderId,
		int selectedStorageId);

}
