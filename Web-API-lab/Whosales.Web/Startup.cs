using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
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

			services.AddTransient<CustomerService>();
			services.AddTransient<ProductService>();
			services.AddTransient<ReleaseReportService>();

			services.AddMediatR(typeof(IWholesaleContext).Assembly);
			services.AddMemoryCache();

			services.AddControllers().AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
		}

		public void Configure(IHostEnvironment environment, IApplicationBuilder app)
		{
			if (environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI();

			}
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
