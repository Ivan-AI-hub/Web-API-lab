using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Whosales.Application.Comands.ReceiptReports;
using Whosales.Application.Queries.ReceiptReports;
using Whosales.Domain;

namespace Whosales.Web.Services
{
	public class ReceiptReportService : BaseTableService<ReceiptReport>
	{
		public ReceiptReportService(IMemoryCache cache, IMediator mediator) : base(cache, mediator)
		{
		}

		public async override void Add(ReceiptReport value)
		{
			await Mediator.Send(new AddReceiptReport(value));
			CacheClear();
		}

		public async override void Delete(int id)
		{
			await Mediator.Send(new DeleteReceiptReport(id));
			CacheClear();
		}

		public override async Task<ReceiptReport?> GetById(int id)
		{
			ReceiptReport? ReceiptReport = await Mediator.Send(new GetReceiptReportById(id));
			return ReceiptReport;
		}

		public override async Task<IEnumerable<ReceiptReport>> GetPage(int pageNumber, Func<ReceiptReport, bool>? whereRule = null, string sortRule = "")
		{
			IEnumerable<ReceiptReport> ReceiptReports;
			Func<ReceiptReport, dynamic> orderRule;
			TranslateSortRule(sortRule, out orderRule);

			SetPageCount(whereRule).Wait();
			ReceiptReports = await Mediator.Send(new GetReceiptReportsQueryPage(PageSystemModel.PageSize, pageNumber, orderRule, whereRule));
			ReceiptReports = ReceiptReports.ToList();

			return ReceiptReports ?? new List<ReceiptReport>();
		}

		public async override Task<IEnumerable<ReceiptReport>> GetAll()
		{
			IEnumerable<ReceiptReport> items;
			string cashKey = "Items";
			if (!Cache.TryGetValue(cashKey, out items))
			{
				items = await Mediator.Send(new GetReceiptReportsQuery());
				items = items.ToList();
				Cache.Set(cashKey, items, TimeSpan.FromSeconds(CacheTime));
			}
			return items;
		}

		public async override void Update(int id, ReceiptReport newValue)
		{
			await Mediator.Send(new UpdateReceiptReport(id, newValue));
			CacheClear();
		}

		private void TranslateSortRule(string sortRule, out Func<ReceiptReport, dynamic> orderRule)
		{
			switch (sortRule)
			{
				case "Volume":
					orderRule = x => x.Volume;
					break;
				case "Cost":
					orderRule = x => x.Cost;
					break;
				case "ReciveDate":
					orderRule = x => x.ReciveDate;
					break;
				case "OrderDate":
					orderRule = x => x.OrderDate;
					break;
				case "DepartureDate":
					orderRule = x => x.DepartureDate;
					break;
				case "EmployerName":
					orderRule = x => x.Employer.Name;
					break;
				case "ProvaiderName":
					orderRule = x => x.Provaider.Name;
					break;
				case "ProductName":
					orderRule = x => x.Product.Name;
					break;
				case "StorageName":
					orderRule = x => x.Storage.Name;
					break;

				default:
					orderRule = x => x.Volume;
					break;
			}
		}

		private async Task SetPageCount(Func<ReceiptReport, bool>? whereRule = null)
		{
			PageSystemModel.PageCount = (await Mediator.Send(new GetReceiptReportsCount(whereRule)) / PageSystemModel.PageSize + 1);
		}
	}
}
