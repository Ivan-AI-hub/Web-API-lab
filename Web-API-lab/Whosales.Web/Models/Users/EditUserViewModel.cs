using Microsoft.AspNetCore.Mvc.Rendering;

namespace Whosales.Web.Models.Users
{
	public class EditUserViewModel
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public int Year { get; set; }
		public IEnumerable<SelectListItem>? Roles { get; set; }
		public string SelectedRole { get; set; }
	}
}
