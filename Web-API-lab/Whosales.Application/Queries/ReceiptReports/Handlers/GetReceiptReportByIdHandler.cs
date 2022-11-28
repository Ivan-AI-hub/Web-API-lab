using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReceiptReports.Handlers
{
	internal class GetReceiptReportByIdHandler : BaseGetHandler<GetReceiptReportById, ReceiptReport?>
	{

		public GetReceiptReportByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<ReceiptReport?> Handle(GetReceiptReportById request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.ReceiptReports
				.Include(x => x.Product)
				.Include(x => x.Storage)
				.Include(x => x.Provaider)
				.Include(x => x.Employer)
				.FirstOrDefault(x => x.ReceiptReportId == request.id));
		}
	}
}
