using MediatR;
using Whosales.Domain;
namespace Whosales.Application.Queries.ReceiptReports
{
	public record GetReceiptReportsQuery : IRequest<IQueryable<ReceiptReport>>;
}
