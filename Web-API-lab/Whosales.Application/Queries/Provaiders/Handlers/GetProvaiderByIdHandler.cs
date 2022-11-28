using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Provaiders.Handlers
{
	internal class GetProvaiderByIdHandler : BaseGetHandler<GetProvaiderById, Provaider?>
	{

		public GetProvaiderByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<Provaider?> Handle(GetProvaiderById request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.Provaiders.Include(x => x.ReceiptReports).FirstOrDefault(x => x.ProvaiderId == request.id));
		}
	}
}
