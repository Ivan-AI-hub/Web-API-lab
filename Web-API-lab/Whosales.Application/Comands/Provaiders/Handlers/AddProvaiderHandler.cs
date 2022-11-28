using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Provaiders.Handlers
{
	internal class AddProvaiderHandler : IRequestHandler<AddProvaider>
	{
		IWholesaleContext _context;
		public AddProvaiderHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddProvaider request, CancellationToken cancellationToken)
		{
			var validator = new ProvaiderValidator();
			var result = validator.Validate(request.Provaider);

			if (result.IsValid)
			{
				_context.Provaiders.Add(request.Provaider);
				_context.SaveChanges();
				return Task.FromResult(new Unit());
			}
			else
			{
				throw new Exception(result.Errors.Select(x => x.ErrorMessage).Aggregate((x, y) => x + y));
			}
		}
	}
}
