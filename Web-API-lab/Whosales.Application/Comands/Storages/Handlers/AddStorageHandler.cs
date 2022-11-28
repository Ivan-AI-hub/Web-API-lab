using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Storages.Handlers
{
	internal class AddStorageHandler : IRequestHandler<AddStorage>
	{
		IWholesaleContext _context;
		public AddStorageHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(AddStorage request, CancellationToken cancellationToken)
		{
			var validator = new StorageValidator();
			var result = validator.Validate(request.Storage);

			if (result.IsValid)
			{
				_context.Storages.Add(request.Storage);
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
