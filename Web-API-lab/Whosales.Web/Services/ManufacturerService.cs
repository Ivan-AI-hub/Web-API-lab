using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Whosales.Application.Comands.Manufacturers;
using Whosales.Application.Queries.Manufacturers;
using Whosales.Domain;

namespace Whosales.Web.Services
{
	public class ManufacturerService : BaseTableService<Manufacturer>
	{
		public ManufacturerService(IMemoryCache cache, IMediator mediator) : base(cache, mediator)
		{
		}

		public async override void Add(Manufacturer value)
		{
			await Mediator.Send(new AddManufacturer(value));
			CacheClear();
		}

		public async override void Delete(int id)
		{
			await Mediator.Send(new DeleteManufacturer(id));
			CacheClear();
		}

		public override async Task<Manufacturer?> GetById(int id)
		{
			Manufacturer? manufacturer = await Mediator.Send(new GetManufacturerById(id));

			return manufacturer;
		}

		public override async Task<IEnumerable<Manufacturer>> GetPage(int pageNumber, Func<Manufacturer, bool>? whereRule = null, string sortRule = "")
		{
			IEnumerable<Manufacturer> manufacturers;
			Func<Manufacturer, dynamic> orderRule;
			TranslateSortRule(sortRule, out orderRule);

			SetPageCount(whereRule).Wait();
			manufacturers = await Mediator.Send(new GetManufacturersQueryPage(PageSystemModel.PageSize, pageNumber, orderRule, whereRule));
			manufacturers = manufacturers.ToList();

			return manufacturers ?? new List<Manufacturer>();
		}

		public async override Task<IEnumerable<Manufacturer>> GetAll()
		{
			IEnumerable<Manufacturer> items;
			string cashKey = "Items";
			if (!Cache.TryGetValue(cashKey, out items))
			{
				items = await Mediator.Send(new GetManufacturersQuery());
				items = items.ToList();
				Cache.Set(cashKey, items, TimeSpan.FromSeconds(CacheTime));
			}
			return items;
		}

		public async override void Update(int id, Manufacturer newValue)
		{
			await Mediator.Send(new UpdateManufacturer(id, newValue));
			CacheClear();
		}

		private void TranslateSortRule(string sortRule, out Func<Manufacturer, dynamic> orderRule)
		{
			if (sortRule == "Name")
			{
				orderRule = x => x.Name;
			}
			else if (sortRule == "ProductsCount")
			{
				orderRule = x => x.Products.Count;
			}
			else
			{
				orderRule = x => x.Name;
			}
		}

		private async Task SetPageCount(Func<Manufacturer, bool>? whereRule = null)
		{
			PageSystemModel.PageCount = (await Mediator.Send(new GetManufacturersCount(whereRule)) / PageSystemModel.PageSize + 1);
		}
	}
}
