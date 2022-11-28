using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Whosales.Domain;
using Whosales.Web.Models.Customers;
using Whosales.Web.Services;

namespace Whosales.Web.Controllers
{
	[Authorize(Roles = "admin")]
	public class CustomerController : BaseController<CustomerService, Customer>
	{
		private static string _previousUrl = "";
		public CustomerController(CustomerService service) : base(service)
		{

		}
		#region Create
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Customer/Add")]
		public ActionResult Add()
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Customer/Add")]
		public IActionResult Add(Customer customer)
		{
			Service.Add(customer);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Update
		[HttpGet]
		[Authorize(Roles = "admin")]
		[Route("Customer/Update/{id}")]
		public async Task<ActionResult> Update(int id)
		{
			_previousUrl = Request.Headers["Referer"].ToString();
			var customer = await Service.GetById(id);
			return View(customer);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		[Route("Customer/Update/{id}")]
		public ActionResult Update(int id, Customer customer)
		{
			Service.Update(id, customer);
			return Redirect(_previousUrl);
		}
		#endregion

		#region Delete
		[Authorize(Roles = "admin")]
		[Route("Customer/Delete/{id}")]
		public ActionResult Delete(int id)
		{
			Service.Delete(id);
			return RedirectToAction("GetAll");
		}
		#endregion

		#region Read
		[Route("Customer")]
		public async Task<ActionResult> GetAll(int currentPage = 1, string? sortRule = "", string? nameTemplate = "", string? addressTemplate = "")
		{
			Service.PageSystemModel.CurrentPage = currentPage;

			sortRule = sortRule ?? "";
			nameTemplate = nameTemplate ?? "";
			addressTemplate = addressTemplate ?? "";

			sortRule = GetValueFromCookie(Request, "customerSort", nameof(sortRule));
			nameTemplate = GetValueFromCookie(Request, "customerName", nameof(nameTemplate));
			addressTemplate = GetValueFromCookie(Request, "customerAddress", nameof(addressTemplate));

			Response.Cookies.Append("customerSort", sortRule);
			Response.Cookies.Append("customerName", nameTemplate);
			Response.Cookies.Append("customerAddress", addressTemplate);


			Func<Customer, bool> whereRule = x => x.Name.Contains(nameTemplate) && x.Address.Contains(addressTemplate);
			var customers = await Service.GetPage(currentPage, whereRule, sortRule);


			var viewModel = new CustomersPageViewModel(customers, Service.PageSystemModel.PageCount, currentPage, sortRule, nameTemplate, addressTemplate);
			return View("CustomersPage", viewModel);
		}

		[Route("Customer/{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			var customer = await Service.GetById(id);
			return View("OneCustomerPage", customer);
		}
		#endregion
	}
}
