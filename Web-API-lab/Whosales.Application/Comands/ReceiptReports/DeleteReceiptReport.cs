using MediatR;

namespace Whosales.Application.Comands.ReceiptReports
{
	public record DeleteReceiptReport(int id) : IRequest;
}
