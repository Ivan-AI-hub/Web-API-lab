using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReceiptReports
{
	public record GetReceiptReportsQueryPage(int pageSize, int pageNumber,
		Func<ReceiptReport, dynamic>? orderByRule = null, Func<ReceiptReport, bool>? whereRule = null)
		: IRequest<IQueryable<ReceiptReport>>;
}
