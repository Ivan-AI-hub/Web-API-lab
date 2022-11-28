using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.Storages;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "user,admin")]
	public class StorageController : BaseController<StorageService, Storage>
	{
		private static string _previousUrl = "";
		public StorageController(StorageService service) : base(service)
		{
		}

		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Storage/Add")]
		public ActionResult Add()
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Storage/Add")]
		public IActionResult Add(Storage Storage)
		{
			Service.Add(Storage);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Storage/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var Storage = await Service.GetById(id);
			return View(Storage);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Storage/Update/{id}")]
		public ActionResult Update(int id, Storage Storage)
		{
			Service.Update(id, Storage);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("Storage/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Authorize(Roles = "user,admin")]
		[Route("Storage")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", string? nameTemplate = "")
		{

			Service.PageSystemModel.CurrentPage = currentPage;
			int number = Service.PageSystemModel.CurrentPage;

			sortRule = sortRule ?? "";
			nameTemplate = nameTemplate ?? "";

			sortRule = GetValueFromCookie(Request, "storageSort", "sortRule");
			nameTemplate = GetValueFromCookie(Request, "storageName", "nameTemplate");

			Response.Cookies.Append("storageSort", sortRule);
			Response.Cookies.Append("storageName", nameTemplate);

			Func<Storage, bool> whereRule = x => x.Name.Contains(nameTemplate);

			var Storages = await Service.GetPage(currentPage, whereRule, sortRule);
			var viewModel = new StoragesPageViewModel(Storages, Service.PageSystemModel.PageCount, currentPage, sortRule, nameTemplate);
			return View("StoragesPage", viewModel);
		}

		[Route("Storage/{id}")]
		[Authorize(Roles = "user,admin")]
		public async Task<ActionResult> GetById(int id)
		{
			var Storage = await Service.GetById(id);
			return View("OneStoragePage", Storage);
		}
		#endregion
	}
}
