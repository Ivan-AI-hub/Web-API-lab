using Microsoft.AspNetCore.Identity;
using Whosales.Web.Models;

namespace Whosales.Web.Middleware
{
	public class UserInitializer
	{
		private bool _isInvoked = false;
		private RequestDelegate _next;

		public UserInitializer(RequestDelegate next)
		{
			_next = next;
		}

		public Task Invoke(HttpContext httpContext)
		{


			if (!_isInvoked)
			{
				CreateUser(httpContext).Wait();

				_isInvoked = true;
			}
			return _next.Invoke(httpContext);
		}
		private async Task CreateUser(HttpContext context)
		{

			UserManager<User> userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
			RoleManager<IdentityRole> roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();
			string adminEmail = "admin@gmail.com";
			string adminName = "admin@gmail.com";

			string password = "_Aa123456";
			if (await roleManager.FindByNameAsync("admin") == null)
			{
				await roleManager.CreateAsync(new IdentityRole("admin"));
			}
			if (await roleManager.FindByNameAsync("user") == null)
			{
				await roleManager.CreateAsync(new IdentityRole("user"));
			}
			if (await userManager.FindByNameAsync(adminEmail) == null)
			{
				User admin = new User
				{
					Email = adminEmail,
					UserName = adminName
				};
				IdentityResult result = await userManager.CreateAsync(admin, password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(admin, "admin");
				}
			}

		}
	}
}
