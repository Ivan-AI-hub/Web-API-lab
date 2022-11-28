using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.Manufacturers;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "user,admin")]
	public class ManufacturerController : BaseController<ManufacturerService, Manufacturer>
	{
		private static string _previousUrl = "";
		public ManufacturerController(ManufacturerService service) : base(service)
		{
		}

		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Manufacturer/Add")]
		public ActionResult Add()
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Manufacturer/Add")]
		public IActionResult Add(Manufacturer manufacturer)
		{
			Service.Add(manufacturer);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Manufacturer/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var manufacturer = await Service.GetById(id);
			return View(manufacturer);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Manufacturer/Update/{id}")]
		public ActionResult Update(int id, Manufacturer manufacturer)
		{
			Service.Update(id, manufacturer);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("Manufacturer/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Authorize(Roles = "user,admin")]
		[Route("Manufacturer")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", string? nameTemplate = "")
		{

			Service.PageSystemModel.CurrentPage = currentPage;
			int number = Service.PageSystemModel.CurrentPage;

			sortRule = sortRule ?? "";
			nameTemplate = nameTemplate ?? "";

			sortRule = GetValueFromCookie(Request, "manufacturerSort", "sortRule");
			nameTemplate = GetValueFromCookie(Request, "manufacturerName", "nameTemplate");

			Response.Cookies.Append("manufacturerSort", sortRule);
			Response.Cookies.Append("manufacturerName", nameTemplate);

			Func<Manufacturer, bool> whereRule = x => x.Name.Contains(nameTemplate);

			var manufacturers = await Service.GetPage(currentPage, whereRule, sortRule);
			var viewModel = new ManufacturersPageViewModel(manufacturers, Service.PageSystemModel.PageCount, currentPage, sortRule, nameTemplate);
			return View("ManufacturersPage", viewModel);
		}

		[Authorize(Roles = "user,admin")]
		[Route("Manufacturer/{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			var manufacturer = await Service.GetById(id);
			return View("OneManufacturerPage", manufacturer);
		}
		#endregion
	}
}
