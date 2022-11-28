using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReceiptReports
{
	public record GetReceiptReportById(int id) : IRequest<ReceiptReport?>;
}
