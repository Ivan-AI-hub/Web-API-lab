using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.ReceiptReports.Handlers
{
	internal class AddReceiptReportHandler : IRequestHandler<AddReceiptReport>
	{
		IWholesaleContext _context;
		public AddReceiptReportHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddReceiptReport request, CancellationToken cancellationToken)
		{
			var validator = new ReceiptReportValidator();
			var result = validator.Validate(request.ReceiptReport);

			if (result.IsValid)
			{
				_context.ReceiptReports.Add(request.ReceiptReport);
				_context.SaveChanges();
				return Task.FromResult(new Unit());
			}
			else
			{
				throw new Exception(result.Errors.Select(x => x.ErrorMessage).Aggregate((x, y) => x + y));
			}
		}
	}
}
