using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.Provaiders.Handlers
{
	internal class DeleteProvaiderHandler : IRequestHandler<DeleteProvaider>
	{
		IWholesaleContext _context;
		public DeleteProvaiderHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteProvaider request, CancellationToken cancellationToken)
		{
			var Provaider = _context.Provaiders.FirstOrDefault(x => x.ProvaiderId == request.id);
			if (Provaider != null)
			{
				_context.Provaiders.Remove(Provaider);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
