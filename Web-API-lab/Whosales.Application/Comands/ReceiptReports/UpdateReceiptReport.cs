using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.ReceiptReports
{
	public record UpdateReceiptReport(int id, ReceiptReport ReceiptReport) : IRequest;
}
