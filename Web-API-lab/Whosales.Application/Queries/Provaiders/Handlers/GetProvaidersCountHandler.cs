using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;

namespace Whosales.Application.Queries.Provaiders.Handlers
{
    internal class GetProvaidersCountHandler : BaseGetHandler<GetProvaidersCount, int>
    {
        public GetProvaidersCountHandler(IWholesaleContext context) : base(context)
        {
        }

        public override Task<int> Handle(GetProvaidersCount request, CancellationToken cancellationToken)
        {
            var whereRule = request.whereRule;
            if (whereRule == null)
            {
                whereRule = x => true;
            }

            return Task.FromResult(_context.Provaiders.Include(x => x.ReceiptReports).Where(whereRule).Count());
        }
    }
}
