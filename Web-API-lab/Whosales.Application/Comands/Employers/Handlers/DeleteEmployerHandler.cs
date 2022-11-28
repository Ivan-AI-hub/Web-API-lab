using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.Employers.Handlers
{
	internal class DeleteEmployerHandler : IRequestHandler<DeleteEmployer>
	{
		IWholesaleContext _context;
		public DeleteEmployerHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteEmployer request, CancellationToken cancellationToken)
		{
			var employer = _context.Employers.FirstOrDefault(x => x.EmployerId == request.id);
			if (employer != null)
			{
				_context.Employers.Remove(employer);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
