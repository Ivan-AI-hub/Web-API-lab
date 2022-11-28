using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.ReceiptReports.Handlers
{
	internal class GetReceiptReportsCountHandler : BaseGetHandler<GetReceiptReportsCount, int>
	{
		public GetReceiptReportsCountHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<int> Handle(GetReceiptReportsCount request, CancellationToken cancellationToken)
		{
			var whereRule = request.whereRule;
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.ReceiptReports
				.Include(x => x.Product)
				.Include(x => x.Storage)
				.Include(x => x.Provaider)
				.Include(x => x.Employer)
				.Where(whereRule).Count());
		}
	}
}
