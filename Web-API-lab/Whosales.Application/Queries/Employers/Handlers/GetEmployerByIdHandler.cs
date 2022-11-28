using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Employers.Handlers
{
	internal class GetEmployerByIdHandler : BaseGetHandler<GetEmployerById, Employer?>
	{
		public GetEmployerByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<Employer?> Handle(GetEmployerById request, CancellationToken cancellationToken)
		{
			var employer = _context.Employers
				.Include(x => x.ReleaseReports)
				.Include(x => x.ReceiptReports)
				.FirstOrDefault(x => x.EmployerId == request.Id);
			return Task.FromResult(employer);
		}
	}
}
