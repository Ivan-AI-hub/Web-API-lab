using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Provaiders.Handlers
{
	internal class UpdateProvaiderHandler : IRequestHandler<UpdateProvaider>
	{
		IWholesaleContext _context;
		public UpdateProvaiderHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateProvaider request, CancellationToken cancellationToken)
		{
			var validator = new ProvaiderValidator();
			var result = validator.Validate(request.Provaider);

			if (result.IsValid)
			{
				request.Provaider.ProvaiderId = request.id;
				_context.Provaiders.Update(request.Provaider);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
