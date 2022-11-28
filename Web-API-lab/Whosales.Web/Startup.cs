using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Persistence;
using Whosales.Web.Middleware;
using Whosales.Web.Models;
using Whosales.Web.Services;

namespace Whosales.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IWholesaleContext, WholesaleContext>();

			services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("UserConnection")));

			services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddDefaultTokenProviders()
				.AddDefaultUI()
				.AddEntityFrameworkStores<ApplicationContext>();

			services.AddTransient<CustomerService>();
			services.AddTransient<EmployerService>();
			services.AddTransient<ManufacturerService>();
			services.AddTransient<ProductTypeService>();
			services.AddTransient<ProvaiderService>();
			services.AddTransient<StorageService>();
			services.AddTransient<ProductService>();
			services.AddTransient<ReceiptReportService>();
			services.AddTransient<ReleaseReportService>();

			services.AddMediatR(typeof(IWholesaleContext).Assembly);

			services.AddControllersWithViews();
		}

		public void Configure(IHostEnvironment environment, IApplicationBuilder app)
		{
			if (environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseMiddleware<UserInitializer>();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapRazorPages();
			});
		}
	}
}
