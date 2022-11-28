using Microsoft.AspNetCore.Mvc.Rendering;

namespace Whosales.Web.Models.Users
{
	public class CreateUserViewModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public int Year { get; set; }
        public IEnumerable<SelectListItem>? Roles { get; set; }
        public string SelectedRole { get; set; }
    }
}
