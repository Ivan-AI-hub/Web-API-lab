using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Whosales.Application.Comands.Provaiders;
using Whosales.Application.Queries.Provaiders;
using Whosales.Domain;

namespace Whosales.Web.Services
{
	public class ProvaiderService : BaseTableService<Provaider>
	{
		public ProvaiderService(IMemoryCache cache, IMediator mediator) : base(cache, mediator)
		{
		}

		public async override void Add(Provaider value)
		{
			await Mediator.Send(new AddProvaider(value));
			CacheClear();
		}

		public async override void Delete(int id)
		{
			await Mediator.Send(new DeleteProvaider(id));
			CacheClear();
		}

		public override async Task<Provaider?> GetById(int id)
		{
			Provaider? Provaider = await Mediator.Send(new GetProvaiderById(id));
			return Provaider;
		}

		public override async Task<IEnumerable<Provaider>> GetPage(int pageNumber, Func<Provaider, bool>? whereRule = null, string sortRule = "")
		{
			IEnumerable<Provaider> Provaiders;
			Func<Provaider, dynamic> orderRule;
			TranslateSortRule(sortRule, out orderRule);

			SetPageCount(whereRule).Wait();
			Provaiders = await Mediator.Send(new GetProvaidersQueryPage(PageSystemModel.PageSize, pageNumber, orderRule, whereRule));
			Provaiders = Provaiders.ToList();

			return Provaiders ?? new List<Provaider>();
		}

		public async override Task<IEnumerable<Provaider>> GetAll()
		{
			IEnumerable<Provaider> items;
			string cashKey = "Items";
			if (!Cache.TryGetValue(cashKey, out items))
			{
				items = await Mediator.Send(new GetProvaidersQuery());
				items = items.ToList();
				Cache.Set(cashKey, items, TimeSpan.FromSeconds(CacheTime));
			}
			return items;
		}

		public async override void Update(int id, Provaider newValue)
		{
			await Mediator.Send(new UpdateProvaider(id, newValue));
			CacheClear();
		}

		private void TranslateSortRule(string sortRule, out Func<Provaider, dynamic> orderRule)
		{
			switch (sortRule)
			{
				case "Name":
					orderRule = x => x.Name;
					break;
				case "ReceiptReports":
					orderRule = x => x.ReceiptReports.Count;
					break;
				default:
					orderRule = x => x.Name;
					break;
			}
		}

		private async Task SetPageCount(Func<Provaider, bool>? whereRule = null)
		{
			PageSystemModel.PageCount = (await Mediator.Send(new GetProvaidersCount(whereRule)) / PageSystemModel.PageSize + 1);
		}
	}
}
