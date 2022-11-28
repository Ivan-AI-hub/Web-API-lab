using MediatR;
using Whosales.Application.Interfaces;

namespace Whosales.Application.Comands.Storages.Handlers
{
	internal class DeleteStorageHandler : IRequestHandler<DeleteStorage>
	{
		IWholesaleContext _context;
		public DeleteStorageHandler(IWholesaleContext context)
		{
			_context = context;
		}

		public Task<Unit> Handle(DeleteStorage request, CancellationToken cancellationToken)
		{
			var Storage = _context.Storages.FirstOrDefault(x => x.StorageId == request.id);
			if (Storage != null)
			{
				_context.Storages.Remove(Storage);
				_context.SaveChanges();
			}


			return Task.FromResult(new Unit());
		}
	}
}
