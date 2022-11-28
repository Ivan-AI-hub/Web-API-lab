using Whosales.Domain;

namespace Whosales.Web.Models.ReceiptReports
{
	public record ReceiptReportsPageViewModel(IEnumerable<ReceiptReport> ReceiptReports,
		IEnumerable<Employer> Employers,
		IEnumerable<Product> Products,
		IEnumerable<Provaider> Provaiders,
		IEnumerable<Storage> Storages,
		int PageCount, int CurrentPage, string SortRule,
		int ProductId, int StorageId);
}
