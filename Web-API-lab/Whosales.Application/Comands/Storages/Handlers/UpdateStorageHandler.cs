using MediatR;
using Whosales.Application.Interfaces;
using Whosales.Application.Validation;

namespace Whosales.Application.Comands.Storages.Handlers
{
	internal class UpdateStorageHandler : IRequestHandler<UpdateStorage>
	{
		IWholesaleContext _context;
		public UpdateStorageHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(UpdateStorage request, CancellationToken cancellationToken)
		{
			var validator = new StorageValidator();
			var result = validator.Validate(request.Storage);

			if (result.IsValid)
			{
				request.Storage.StorageId = request.id;
				_context.Storages.Update(request.Storage);
				_context.SaveChanges();
			}

			return Task.FromResult(new Unit());
		}
	}
}
