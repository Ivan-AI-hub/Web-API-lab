using Microsoft.EntityFrameworkCore;
using Whosales.Domain;

namespace Whosales.Application.Interfaces
{
	public interface IWholesaleContext
	{
		DbSet<Customer> Customers { get; set; }
		DbSet<Employer> Employers { get; set; }
		DbSet<Manufacturer> Manufacturers { get; set; }
		DbSet<ManufacturersInformation> ManufacturersInformations { get; set; }
		DbSet<ProductBalance> ProductBalances { get; set; }
		DbSet<Product> Products { get; set; }
		DbSet<ProductType> ProductTypes { get; set; }
		DbSet<Provaider> Provaiders { get; set; }
		DbSet<ProvidersInformation> ProvidersInformations { get; set; }
		DbSet<ReceiptReport> ReceiptReports { get; set; }
		DbSet<ReleaseReport> ReleaseReports { get; set; }
		DbSet<Storage> Storages { get; set; }

		public int SaveChanges();
	}
}