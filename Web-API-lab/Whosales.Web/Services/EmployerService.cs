using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Whosales.Application.Comands.Employers;
using Whosales.Application.Queries.Employers;
using Whosales.Domain;

namespace Whosales.Web.Services
{
	public class EmployerService : BaseTableService<Employer>
	{
		public EmployerService(IMemoryCache cache, IMediator mediator) : base(cache, mediator)
		{
		}

		public async override void Add(Employer value)
		{
			await Mediator.Send(new AddEmployer(value));
			CacheClear();
		}

		public async override void Delete(int id)
		{
			await Mediator.Send(new DeleteEmployer(id));
			CacheClear();
		}

		public override async Task<Employer?> GetById(int id)
		{
			Employer? employer = await Mediator.Send(new GetEmployerById(id));
			return employer;
		}

		public override async Task<IEnumerable<Employer>> GetPage(int pageNumber, Func<Employer, bool>? whereRule = null, string sortRule = "")
		{
			IEnumerable<Employer> employers;
			Func<Employer, dynamic> orderRule;
			TranslateSortRule(sortRule, out orderRule);

			SetPageCount(whereRule).Wait();
			employers = await Mediator.Send(new GetEmployersQueryPage(PageSystemModel.PageSize, pageNumber, orderRule, whereRule));
			employers = employers.ToList();

			return employers ?? new List<Employer>();
		}

		public async override Task<IEnumerable<Employer>> GetAll()
		{
			IEnumerable<Employer> items;
			string cashKey = "Items";
			if (!Cache.TryGetValue(cashKey, out items))
			{
				items = await Mediator.Send(new GetEmployersQuery());
				items = items.ToList();
				Cache.Set(cashKey, items, TimeSpan.FromSeconds(CacheTime));
			}
			return items;
		}

		public async override void Update(int id, Employer newValue)
		{
			await Mediator.Send(new UpdateEmployer(id, newValue));
			CacheClear();
		}

		private void TranslateSortRule(string sortRule, out Func<Employer, dynamic> orderRule)
		{
			switch (sortRule)
			{
				case "Name":
					orderRule = x => x.Name;
					break;
				case "RcVol":
					orderRule = x => x.ReceiptReports.Sum(x => x.Volume);
					break;
				case "RcCost":
					orderRule = x => x.ReceiptReports.Sum(x => x.Cost);
					break;
				case "RlVol":
					orderRule = x => x.ReleaseReports.Sum(x => x.Volume);
					break;
				case "RlCost":
					orderRule = x => x.ReleaseReports.Sum(x => x.Cost);
					break;
				default:
					orderRule = x => x.Name;
					break;
			}
		}

		private async Task SetPageCount(Func<Employer, bool>? whereRule = null)
		{
			PageSystemModel.PageCount = (await Mediator.Send(new GetEmployersCount(whereRule)) / PageSystemModel.PageSize + 1);
		}
	}
}
