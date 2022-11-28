using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Storages.Handlers
{
	internal class GetStoragesQueryHandler : BaseGetHandler<GetStoragesQuery, IQueryable<Storage>>
	{

		public GetStoragesQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Storage>> Handle(GetStoragesQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.Storages
				.Include(x => x.ReleaseReports)
				.Include(x => x.ReceiptReports)
				.AsQueryable());
		}
	}
}
