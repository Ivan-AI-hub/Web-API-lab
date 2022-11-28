using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.Storages.Handlers
{
	internal class GetStoragesCountHandler : BaseGetHandler<GetStoragesCount, int>
	{
		public GetStoragesCountHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<int> Handle(GetStoragesCount request, CancellationToken cancellationToken)
		{
			var whereRule = request.whereRule;
			if (whereRule == null)
			{
				whereRule = x => true;
			}

			return Task.FromResult(_context.Storages
				.Include(x => x.ReleaseReports)
				.Include(x => x.ReceiptReports)
				.Where(whereRule).Count());
		}
	}
}
