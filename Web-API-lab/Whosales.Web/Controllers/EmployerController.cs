using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.Employers;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "admin")]
	public class EmployerController : BaseController<EmployerService, Employer>
	{
		private static string _previousUrl = "";
		public EmployerController(EmployerService service) : base(service)
		{
		}
		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Employer/Add")]
		public ActionResult Add()
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Employer/Add")]
		public IActionResult Add(Employer employer)
		{
			Service.Add(employer);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Employer/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var employer = await Service.GetById(id);
			return View(employer);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Employer/Update/{id}")]
		public ActionResult Update(int id, Employer employer)
		{
			Service.Update(id, employer);
			return Redirect(_previousUrl);

		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("Employer/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Authorize(Roles = "user,admin")]
		[Route("Employer/")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", string? nameTemplate = "")
		{

			Service.PageSystemModel.CurrentPage = currentPage;
			int number = Service.PageSystemModel.CurrentPage;

			sortRule = sortRule ?? "";
			nameTemplate = nameTemplate ?? "";

			sortRule = GetValueFromCookie(Request, "employerSort", "sortRule");
			nameTemplate = GetValueFromCookie(Request, "employerName", "nameTemplate");

			Response.Cookies.Append("employerSort", sortRule);
			Response.Cookies.Append("employerName", nameTemplate);

			Func<Employer, bool> whereRule = x => x.Name.Contains(nameTemplate);

			var employers = await Service.GetPage(currentPage, whereRule, sortRule);
			var viewModel = new EmployersPageViewModel(employers, Service.PageSystemModel.PageCount, currentPage, sortRule, nameTemplate);
			return View("EmployersPage", viewModel);
		}

		[Route("Employer/{id}")]
		[Authorize(Roles = "user,admin")]
		public async Task<ActionResult> GetById(int id)
		{
			var employer = await Service.GetById(id);
			return View("OneEmployerPage", employer);
		}
		#endregion
	}
}
