[assembly: HostingStartup(typeof(Whosales.Web.Areas.Identity.IdentityHostingStartup))]
namespace Whosales.Web.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
			});
		}
	}
}
