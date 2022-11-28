using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Whosales.Application.Comands.Storages;
using Whosales.Application.Queries.Storages;
using Whosales.Domain;

namespace Whosales.Web.Services
{
	public class StorageService : BaseTableService<Storage>
	{
		public StorageService(IMemoryCache cache, IMediator mediator) : base(cache, mediator)
		{
		}

		public async override void Add(Storage value)
		{
			await Mediator.Send(new AddStorage(value));
			CacheClear();
		}

		public async override void Delete(int id)
		{
			await Mediator.Send(new DeleteStorage(id));
			CacheClear();
		}

		public override async Task<Storage?> GetById(int id)
		{
			Storage? Storage = await Mediator.Send(new GetStorageById(id));
			return Storage;
		}

		public override async Task<IEnumerable<Storage>> GetPage(int pageNumber, Func<Storage, bool>? whereRule = null, string sortRule = "")
		{
			IEnumerable<Storage> Storages;
			Func<Storage, dynamic> orderRule;
			TranslateSortRule(sortRule, out orderRule);

			SetPageCount(whereRule).Wait();
			Storages = await Mediator.Send(new GetStoragesQueryPage(PageSystemModel.PageSize, pageNumber, orderRule, whereRule));
			Storages = Storages.ToList();

			return Storages ?? new List<Storage>();
		}

		public async override Task<IEnumerable<Storage>> GetAll()
		{
			IEnumerable<Storage> items;
			string cashKey = "Items";
			if (!Cache.TryGetValue(cashKey, out items))
			{
				items = await Mediator.Send(new GetStoragesQuery());
				items = items.ToList();
				Cache.Set(cashKey, items, TimeSpan.FromSeconds(CacheTime));
			}
			return items;
		}

		public async override void Update(int id, Storage newValue)
		{
			await Mediator.Send(new UpdateStorage(id, newValue));
			CacheClear();
		}

		private void TranslateSortRule(string sortRule, out Func<Storage, dynamic> orderRule)
		{
			switch (sortRule)
			{
				case "Name":
					orderRule = x => x.Name;
					break;
				case "receiptVolume":
					orderRule = x => x.ReceiptReports.Sum(x => x.Volume);
					break;
				case "releaseVolume":
					orderRule = x => x.ReleaseReports.Sum(x => x.Volume);
					break;
				default:
					orderRule = x => x.Name;
					break;
			}
		}

		private async Task SetPageCount(Func<Storage, bool>? whereRule = null)
		{
			PageSystemModel.PageCount = (await Mediator.Send(new GetStoragesCount(whereRule)) / PageSystemModel.PageSize + 1);
		}
	}
}
