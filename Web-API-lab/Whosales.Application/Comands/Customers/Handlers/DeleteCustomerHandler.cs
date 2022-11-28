using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.Customers.Handlers
{
	internal class DeleteCustomerHandler : IRequestHandler<DeleteCustomer>
	{
		IWholesaleContext _context;
		public DeleteCustomerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteCustomer request, CancellationToken cancellationToken)
		{
			var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == request.id);
			if (customer != null)
			{
				_context.Customers.Remove(customer);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
