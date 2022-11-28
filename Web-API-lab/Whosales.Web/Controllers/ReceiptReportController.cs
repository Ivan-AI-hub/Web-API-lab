using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.ReceiptReports;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "user,admin")]
	public class ReceiptReportController : BaseController<ReceiptReportService, ReceiptReport>
	{
		private static string _previousUrl = "";
		private EmployerService _employerService;
		private ProductService _productService;
		private ProvaiderService _provaiderService;
		private StorageService _storageService;
		public ReceiptReportController(ReceiptReportService service,
			EmployerService employerService,
			ProductService productService,
			ProvaiderService provaiderService,
			StorageService storageService) : base(service)
		{
			_employerService = employerService;
			_productService = productService;
			_provaiderService = provaiderService;
			_storageService = storageService;
		}

		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("ReceiptReport/Add")]
		public async Task<IActionResult> Add(int selectedEmployerId = 0, int selectedProductId = 0, int selectedProvaiderId = 0, int selectedStorageId = 0)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var employers = await _employerService.GetAll();
			var products = await _productService.GetAll();
			var provaiders = await _provaiderService.GetAll();
			var storages = await _storageService.GetAll();
			var viewModel = new ReceiptReportAddPageViewModel(employers, products, provaiders, storages, selectedEmployerId, selectedProductId, selectedProvaiderId, selectedStorageId);
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("ReceiptReport/Add")]
		public IActionResult Add(ReceiptReport ReceiptReport)
		{
			Service.Add(ReceiptReport);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("ReceiptReport/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var ReceiptReport = await Service.GetById(id);
			var employers = await _employerService.GetAll();
			var products = await _productService.GetAll();
			var provaiders = await _provaiderService.GetAll();
			var storages = await _storageService.GetAll();
			var viewModel = new ReceiptReportUpdatePageViewModel(ReceiptReport, employers, products, provaiders, storages);
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("ReceiptReport/Update/{id}")]
		public ActionResult Update(int id, ReceiptReport ReceiptReport)
		{
			Service.Update(id, ReceiptReport);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("ReceiptReport/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Authorize(Roles = "user,admin")]
		[Route("ReceiptReport")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", int? productId = null, int? storageId = null)
		{
			Service.PageSystemModel.CurrentPage = currentPage;

			sortRule = sortRule ?? "";
			productId = productId ?? 0;
			storageId = storageId ?? 0;

			sortRule = GetValueFromCookie(Request, "RecRepSort", "sortRule");
			productId = int.Parse(GetValueFromCookie(Request, "RecRepProductId", "productId", "0"));
			storageId = int.Parse(GetValueFromCookie(Request, "RecRepStorageId", "storageId", "0"));

			Response.Cookies.Append("RecRepSort", sortRule);
			Response.Cookies.Append("RecRepProductId", productId.ToString());
			Response.Cookies.Append("RecRepStorageId", storageId.ToString());


			Func<ReceiptReport, bool> whereRule = x =>
											(productId == 0 || x.Product.ProductId == productId) &&
											(storageId == 0 || x.Storage.StorageId == storageId);

			var ReceiptReports = await Service.GetPage(currentPage, whereRule, sortRule);
			var employers = await _employerService.GetAll();
			var products = await _productService.GetAll();
			var provaiders = await _provaiderService.GetAll();
			var storages = await _storageService.GetAll();

			var viewModel = new ReceiptReportsPageViewModel(ReceiptReports, employers, products, provaiders, storages,
													Service.PageSystemModel.PageCount, currentPage,
													sortRule,
													(int)productId, (int)storageId);
			return View("ReceiptReportsPage", viewModel);
		}

		[Route("ReceiptReport/{id}")]
		[Authorize(Roles = "user,admin")]
		public async Task<ActionResult> GetById(int id)
		{
			var ReceiptReport = await Service.GetById(id);
			return View("OneReceiptReportPage", ReceiptReport);
		}
		#endregion
	}
}
