using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Customers.Handlers
{
	internal class AddCustomerHandler : IRequestHandler<AddCustomer>
	{
		IWholesaleContext _context;
		public AddCustomerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddCustomer request, CancellationToken cancellationToken)
		{
			var validator = new CustomerValidator();
			var result = validator.Validate(request.customer);
			if (result.IsValid)
			{
				_context.Customers.Add(request.customer);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}

