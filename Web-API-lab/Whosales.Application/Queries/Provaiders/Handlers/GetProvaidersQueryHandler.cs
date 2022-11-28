using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Provaiders.Handlers
{
	internal class GetProvaidersQueryHandler : BaseGetHandler<GetProvaidersQuery, IQueryable<Provaider>>
	{

		public GetProvaidersQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Provaider>> Handle(GetProvaidersQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.Provaiders.Include(x => x.ReceiptReports).AsQueryable());
		}
	}
}
