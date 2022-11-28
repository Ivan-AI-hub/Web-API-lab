using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReceiptReports
{
	public record GetReceiptReportsCount(Func<ReceiptReport, bool>? whereRule = null) : IRequest<int>;
}
