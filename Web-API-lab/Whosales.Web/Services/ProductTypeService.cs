using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Whosales.Application.Comands.ProductTypes;
using Whosales.Application.Queries.ProductTypes;
using Whosales.Domain;

namespace Whosales.Web.Services
{
	public class ProductTypeService : BaseTableService<ProductType>
	{
		public ProductTypeService(IMemoryCache cache, IMediator mediator) : base(cache, mediator)
		{
		}

		public async override void Add(ProductType value)
		{
			await Mediator.Send(new AddProductType(value));
			CacheClear();
		}

		public async override void Delete(int id)
		{
			await Mediator.Send(new DeleteProductType(id));
			CacheClear();
		}

		public override async Task<ProductType?> GetById(int id)
		{
			ProductType? ProductType = await Mediator.Send(new GetProductTypeById(id));
			return ProductType;
		}

		public override async Task<IEnumerable<ProductType>> GetPage(int pageNumber, Func<ProductType, bool>? whereRule = null, string sortRule = "")
		{
			IEnumerable<ProductType> ProductTypes;
			Func<ProductType, dynamic> orderRule;
			TranslateSortRule(sortRule, out orderRule);

			SetPageCount(whereRule).Wait();
			ProductTypes = await Mediator.Send(new GetProductTypesQueryPage(PageSystemModel.PageSize, pageNumber, orderRule, whereRule));
			ProductTypes = ProductTypes.ToList();

			return ProductTypes ?? new List<ProductType>();
		}

		public async override Task<IEnumerable<ProductType>> GetAll()
		{
			IEnumerable<ProductType> items;
			string cashKey = "Items";
			if (!Cache.TryGetValue(cashKey, out items))
			{
				items = await Mediator.Send(new GetProductTypesQuery());
				items = items.ToList();
				Cache.Set(cashKey, items, TimeSpan.FromSeconds(CacheTime));
			}
			return items;
		}

		public async override void Update(int id, ProductType newValue)
		{
			await Mediator.Send(new UpdateProductType(id, newValue));
			CacheClear();
		}

		private void TranslateSortRule(string sortRule, out Func<ProductType, dynamic> orderRule)
		{
			switch (sortRule)
			{
				case "Name":
					orderRule = x => x.Name;
					break;
				case "Description":
					orderRule = x => x.Description;
					break;
				case "Feature":
					orderRule = x => x.Feature;
					break;
				default:
					orderRule = x => x.Name;
					break;
			}
		}

		private async Task SetPageCount(Func<ProductType, bool>? whereRule = null)
		{
			PageSystemModel.PageCount = (await Mediator.Send(new GetProductTypesCount(whereRule)) / PageSystemModel.PageSize + 1);
		}
	}
}
