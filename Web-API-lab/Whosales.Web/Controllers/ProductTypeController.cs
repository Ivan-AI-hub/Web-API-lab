using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.ProductTypes;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "user,admin")]
	public class ProductTypeController : BaseController<ProductTypeService, ProductType>
	{
		private static string _previousUrl = "";
		public ProductTypeController(ProductTypeService service) : base(service)
		{
		}

		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("ProductType/Add")]
		public ActionResult Add()
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("ProductType/Add")]
		public IActionResult Add(ProductType ProductType)
		{
			Service.Add(ProductType);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("ProductType/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var ProductType = await Service.GetById(id);
			return View(ProductType);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("ProductType/Update/{id}")]
		public ActionResult Update(int id, ProductType ProductType)
		{
			Service.Update(id, ProductType);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("ProductType/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Authorize(Roles = "user,admin")]
		[Route("ProductType")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", string? nameTemplate = "", string? featureTemplate = "")
		{

			Service.PageSystemModel.CurrentPage = currentPage;
			int number = Service.PageSystemModel.CurrentPage;

			sortRule = sortRule ?? "";
			nameTemplate = nameTemplate ?? "";
			featureTemplate = featureTemplate ?? "";

			sortRule = GetValueFromCookie(Request, "PrTySort", "sortRule");
			nameTemplate = GetValueFromCookie(Request, "PrTyName", "nameTemplate");
			featureTemplate = GetValueFromCookie(Request, "PrTyFeature", "featureTemplate");

			Response.Cookies.Append("PrTySort", sortRule);
			Response.Cookies.Append("PrTyName", nameTemplate);
			Response.Cookies.Append("PrTyFeature", featureTemplate);

			Func<ProductType, bool> whereRule = x => x.Name.Contains(nameTemplate);

			var ProductTypes = await Service.GetPage(currentPage, whereRule, sortRule);
			var viewModel = new ProductTypesPageViewModel(ProductTypes, Service.PageSystemModel.PageCount, currentPage, sortRule, nameTemplate, featureTemplate);
			return View("ProductTypesPage", viewModel);
		}

		[Authorize(Roles = "user,admin")]
		[Route("ProductType/{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			var ProductType = await Service.GetById(id);
			return View("OneProductTypePage", ProductType);
		}
		#endregion
	}
}
