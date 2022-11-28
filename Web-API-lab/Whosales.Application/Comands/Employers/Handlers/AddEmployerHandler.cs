using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Employers.Handlers
{
	internal class AddEmployerHandler : IRequestHandler<AddEmployer>
	{
		IWholesaleContext _context;
		public AddEmployerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddEmployer request, CancellationToken cancellationToken)
		{
			var validator = new EmployerValidator();
			var result = validator.Validate(request.Employer);

			if (result.IsValid)
			{
				_context.Employers.Add(request.Employer);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
