using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.ReceiptReports.Handlers
{
	internal class DeleteReceiptReportHandler : IRequestHandler<DeleteReceiptReport>
	{
		IWholesaleContext _context;
		public DeleteReceiptReportHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteReceiptReport request, CancellationToken cancellationToken)
		{
			var ReceiptReport = _context.ReceiptReports.FirstOrDefault(x => x.ReceiptReportId == request.id);
			if (ReceiptReport != null)
			{
				_context.ReceiptReports.Remove(ReceiptReport);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
