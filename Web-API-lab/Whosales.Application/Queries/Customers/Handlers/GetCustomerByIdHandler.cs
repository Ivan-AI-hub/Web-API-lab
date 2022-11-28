using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Customers.Handlers
{
	internal class GetCustomerByIdHandler : BaseGetHandler<GetCustomerById, Customer?>
	{

		public GetCustomerByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<Customer?> Handle(GetCustomerById request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.Customers.Include(x => x.ReleaseReports).FirstOrDefault(x => x.CustomerId == request.id));
		}
	}
}
