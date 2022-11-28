using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Employers.Handlers
{
	internal class UpdateEmployerHandler : IRequestHandler<UpdateEmployer>
	{
		IWholesaleContext _context;
		public UpdateEmployerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateEmployer request, CancellationToken cancellationToken)
		{
			var validator = new EmployerValidator();
			var result = validator.Validate(request.Employer);

			if (result.IsValid)
			{
				request.Employer.EmployerId = request.id;
				_context.Employers.Update(request.Employer);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
