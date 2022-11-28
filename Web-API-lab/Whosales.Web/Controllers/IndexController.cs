using Microsoft.AspNetCore.Mvc;

namespace Whosales.Web.Controllers
{
	public class IndexController : Controller
	{
		[Route("/")]
		public async Task<ActionResult> Main()
		{
			return await Task.FromResult(View());
		}
	}
}
