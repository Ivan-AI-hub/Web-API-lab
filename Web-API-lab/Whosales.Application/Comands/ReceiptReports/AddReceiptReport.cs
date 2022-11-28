using MediatR;
using Whosales.Domain;

namespace Whosales.Application.Comands.ReceiptReports
{
	public record AddReceiptReport(ReceiptReport ReceiptReport) : IRequest;
}
