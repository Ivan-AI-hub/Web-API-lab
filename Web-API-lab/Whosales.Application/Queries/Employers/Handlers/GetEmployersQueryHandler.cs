using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Employers.Handlers
{
	internal class GetEmployersQueryHandler : BaseGetHandler<GetEmployersQuery, IQueryable<Employer>>
	{
		public GetEmployersQueryHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<IQueryable<Employer>> Handle(GetEmployersQuery request, CancellationToken cancellationToken)
		{
			var employers = _context.Employers.Include(x => x.ReleaseReports)
											  .Include(x => x.ReceiptReports)
											  .AsQueryable();
			return Task.FromResult(employers);
		}
	}
}
