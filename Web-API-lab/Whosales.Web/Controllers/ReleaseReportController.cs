using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.ReleaseReports;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "user,admin")]
	public class ReleaseReportController : BaseController<ReleaseReportService, ReleaseReport>
	{
		private static string _previousUrl = "";
		private EmployerService _employerService;
		private ProductService _productService;
		private CustomerService _customerService;
		private StorageService _storageService;
		public ReleaseReportController(ReleaseReportService service,
			EmployerService employerService,
			ProductService productService,
			CustomerService customerService,
			StorageService storageService) : base(service)
		{
			_employerService = employerService;
			_productService = productService;
			_customerService = customerService;
			_storageService = storageService;
		}

		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("ReleaseReport/Add")]
		public async Task<IActionResult> Add(int selectedEmployerId = 0, int selectedProductId = 0, int selectedCustomerId = 0, int selectedStorageId = 0)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var employers = await _employerService.GetAll();
			var products = await _productService.GetAll();
			var Customers = await _customerService.GetAll();
			var storages = await _storageService.GetAll();
			var viewModel = new ReleaseReportAddPageViewModel(employers, products, Customers, storages, selectedEmployerId, selectedProductId, selectedCustomerId, selectedStorageId);
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("ReleaseReport/Add")]
		public IActionResult Add(ReleaseReport ReleaseReport)
		{
			Service.Add(ReleaseReport);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("ReleaseReport/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var ReleaseReport = await Service.GetById(id);
			var employers = await _employerService.GetAll();
			var products = await _productService.GetAll();
			var Customers = await _customerService.GetAll();
			var storages = await _storageService.GetAll();
			var viewModel = new ReleaseReportUpdatePageViewModel(ReleaseReport, employers, products, Customers, storages);
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("ReleaseReport/Update/{id}")]
		public ActionResult Update(int id, ReleaseReport ReleaseReport)
		{
			Service.Update(id, ReleaseReport);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("ReleaseReport/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Authorize(Roles = "user,admin")]
		[Route("ReleaseReport")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", int? productId = null, int? storageId = null)
		{
			Service.PageSystemModel.CurrentPage = currentPage;

			sortRule = sortRule ?? "";
			productId = productId ?? 0;
			storageId = storageId ?? 0;

			sortRule = GetValueFromCookie(Request, "RelRepSort", "sortRule");
			productId = int.Parse(GetValueFromCookie(Request, "RelRepProductId", "productId", "0"));
			storageId = int.Parse(GetValueFromCookie(Request, "RelRepStorageId", "storageId", "0"));

			Response.Cookies.Append("RelRepSort", sortRule);
			Response.Cookies.Append("RelRepProductId", productId.ToString());
			Response.Cookies.Append("RelRepStorageId", storageId.ToString());


			Func<ReleaseReport, bool> whereRule = x =>
											(productId == 0 || x.Product.ProductId == productId) &&
											(storageId == 0 || x.Storage.StorageId == storageId);

			var ReleaseReports = await Service.GetPage(currentPage, whereRule, sortRule);
			var employers = await _employerService.GetAll();
			var products = await _productService.GetAll();
			var Customers = await _customerService.GetAll();
			var storages = await _storageService.GetAll();

			var viewModel = new ReleaseReportsPageViewModel(ReleaseReports, employers, products, Customers, storages,
													Service.PageSystemModel.PageCount, currentPage,
													sortRule,
													(int)productId, (int)storageId);
			return View("ReleaseReportsPage", viewModel);
		}

		[Route("ReleaseReport/{id}")]
		[Authorize(Roles = "user,admin")]
		public async Task<ActionResult> GetById(int id)
		{
			var ReleaseReport = await Service.GetById(id);
			return View("OneReleaseReportPage", ReleaseReport);
		}
		#endregion
	}
}
