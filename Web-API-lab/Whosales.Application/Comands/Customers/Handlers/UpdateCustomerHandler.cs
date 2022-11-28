using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Customers.Handlers
{
	internal class UpdateCustomerHandler : IRequestHandler<UpdateCustomer>
	{
		IWholesaleContext _context;
		public UpdateCustomerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateCustomer request, CancellationToken cancellationToken)
		{
			var validator = new CustomerValidator();
			var result = validator.Validate(request.customer);

			if (result.IsValid)
			{
				request.customer.CustomerId = request.id;
				_context.Customers.Update(request.customer);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
