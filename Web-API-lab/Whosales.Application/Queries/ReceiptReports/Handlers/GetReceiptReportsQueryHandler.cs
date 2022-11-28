using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.ReceiptReports.Handlers
{
	internal class GetReceiptReportsQueryHandler : BaseGetHandler<GetReceiptReportsQuery, IQueryable<ReceiptReport>>
	{

		public GetReceiptReportsQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<ReceiptReport>> Handle(GetReceiptReportsQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.ReceiptReports
				.Include(x => x.Product)
				.Include(x => x.Storage)
				.Include(x => x.Provaider)
				.Include(x => x.Employer)
				.AsQueryable());
		}
	}
}
