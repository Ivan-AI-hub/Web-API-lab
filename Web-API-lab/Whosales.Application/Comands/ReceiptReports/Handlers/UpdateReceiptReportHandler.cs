using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.ReceiptReports.Handlers
{
	internal class UpdateReceiptReportHandler : IRequestHandler<UpdateReceiptReport>
	{
		IWholesaleContext _context;
		public UpdateReceiptReportHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateReceiptReport request, CancellationToken cancellationToken)
		{
			var validator = new ReceiptReportValidator();
			var result = validator.Validate(request.ReceiptReport);

			if (result.IsValid)
			{
				request.ReceiptReport.ReceiptReportId = request.id;
				_context.ReceiptReports.Update(request.ReceiptReport);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
