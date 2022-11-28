using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.Provaiders;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "admin")]
	public class ProvaiderController : BaseController<ProvaiderService, Provaider>
	{
		private static string _previousUrl = "";
		public ProvaiderController(ProvaiderService service) : base(service)
		{

		}
		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Provaider/Add")]
		public ActionResult Add()
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Provaider/Add")]
		public IActionResult Add(Provaider Provaider)
		{
			Service.Add(Provaider);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Provaider/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var Provaider = await Service.GetById(id);
			return View(Provaider);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Provaider/Update/{id}")]
		public ActionResult Update(int id, Provaider Provaider)
		{
			Service.Update(id, Provaider);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("Provaider/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Route("Provaider")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", string? nameTemplate = "", string? addressTemplate = "")
		{
			Service.PageSystemModel.CurrentPage = currentPage;

			sortRule = sortRule ?? "";
			nameTemplate = nameTemplate ?? "";
			addressTemplate = addressTemplate ?? "";

			sortRule = GetValueFromCookie(Request, "provaiderSort", "sortRule");
			nameTemplate = GetValueFromCookie(Request, "provaiderName", "nameTemplate");
			addressTemplate = GetValueFromCookie(Request, "provaiderAddress", "addressTemplate");

			Response.Cookies.Append("provaiderSort", sortRule);
			Response.Cookies.Append("provaiderName", nameTemplate);
			Response.Cookies.Append("provaiderAddress", addressTemplate);


			Func<Provaider, bool> whereRule = x => x.Name.Contains(nameTemplate) && x.Address.Contains(addressTemplate);
			var Provaiders = await Service.GetPage(currentPage, whereRule, sortRule);


			var viewModel = new ProvaidersPageViewModel(Provaiders, Service.PageSystemModel.PageCount, currentPage, sortRule, nameTemplate, addressTemplate);
			return View("ProvaidersPage", viewModel);
		}

		[Route("Provaider/{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			var Provaider = await Service.GetById(id);
			return View("OneProvaiderPage", Provaider);
		}
		#endregion
	}
}
