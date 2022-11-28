using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Application.Queries.Base;
using Whosales.Domain;

namespace Whosales.Application.Queries.Storages.Handlers
{
	internal class GetStorageByIdHandler : BaseGetHandler<GetStorageById, Storage?>
	{

		public GetStorageByIdHandler(IWholesaleContext context) : base(context)
		{
		}

		public override Task<Storage?> Handle(GetStorageById request, CancellationToken cancellationToken)
		{
			return Task.FromResult(_context.Storages
				.Include(x => x.ReleaseReports)
				.Include(x => x.ReceiptReports)
				.FirstOrDefault(x => x.StorageId == request.id));
		}
	}
}
