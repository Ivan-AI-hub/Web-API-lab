using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.Products;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "user,admin")]
	public class ProductController : BaseController<ProductService, Product>
	{
		private static string _previousUrl = "";
		private ManufacturerService _manufacturerService;
		private ProductTypeService _productTypeService;
		public ProductController(ProductService service, ManufacturerService manufacturerService, ProductTypeService productTypeService) : base(service)
		{
			_manufacturerService = manufacturerService;
			_productTypeService = productTypeService;
		}

		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Product/Add")]
		public async Task<IActionResult> Add(int selectedManufacturerId = 0)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var manufacturers = await _manufacturerService.GetAll();
			var types = await _productTypeService.GetAll();
			var viewModel = new ProductAddPageViewModel(manufacturers, types, selectedManufacturerId);
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Product/Add")]
		public IActionResult Add(Product product)
		{
			Service.Add(product);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Product/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var product = await Service.GetById(id);
			var manufacturers = await _manufacturerService.GetAll();
			var types = await _productTypeService.GetAll();
			var viewModel = new ProductUpdatePageViewModel(product, manufacturers, types);
			return View(viewModel);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Product/Update/{id}")]
		public ActionResult Update(int id, Product product)
		{
			Service.Update(id, product);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("Product/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Authorize(Roles = "user,admin")]
		[Route("Product")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", string? nameTemplate = "", int? manufacturerId = null, int? typeId = null)
		{
			Service.PageSystemModel.CurrentPage = currentPage;

			sortRule = sortRule ?? "";
			nameTemplate = nameTemplate ?? "";
			manufacturerId = manufacturerId ?? 0;
			typeId = typeId ?? 0;

			sortRule = GetValueFromCookie(Request, "productSort", "sortRule");
			nameTemplate = GetValueFromCookie(Request, "productName", "nameTemplate");
			manufacturerId = int.Parse(GetValueFromCookie(Request, "manufacturerId", "manufacturerId", "0"));
			typeId = int.Parse(GetValueFromCookie(Request, "typeId", "typeId", "0"));

			Response.Cookies.Append("productSort", sortRule);
			Response.Cookies.Append("productName", nameTemplate);
			Response.Cookies.Append("manufacturerId", manufacturerId.ToString());
			Response.Cookies.Append("typeId", typeId.ToString());


			Func<Product, bool> whereRule = x => x.Name.Contains(nameTemplate) &&
											(manufacturerId == 0 || x.Manufacturer.ManufacturerId == manufacturerId) &&
											(typeId == 0 || x.Type.ProductTypeId == typeId);

			var products = await Service.GetPage(currentPage, whereRule, sortRule);
			var manufacturers = await _manufacturerService.GetAll();
			var types = await _productTypeService.GetAll();

			var viewModel = new ProductsPageViewModel(products, manufacturers, types,
													Service.PageSystemModel.PageCount, currentPage,
													sortRule, nameTemplate,
													(int)manufacturerId, (int)typeId);
			return View("ProductsPage", viewModel);
		}

		[Route("Product/{id}")]
		[Authorize(Roles = "user,admin")]
		public async Task<ActionResult> GetById(int id)
		{
			var product = await Service.GetById(id);
			return View("OneProductPage", product);
		}
		#endregion
	}
}
