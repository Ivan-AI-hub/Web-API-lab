using Microsoft.EntityFrameworkCore;
using Whosales.Application.Interfaces;
using Whosales.Domain;
using Whosales.Persistence.EntityConfiguration;

namespace Whosales.Persistence
{
	public partial class WholesaleContext : DbContext, IWholesaleContext
	{
		public WholesaleContext()
		{
		}

		public WholesaleContext(DbContextOptions<WholesaleContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Customer> Customers { get; set; } = null!;
		public virtual DbSet<Employer> Employers { get; set; } = null!;
		public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
		public virtual DbSet<ManufacturersInformation> ManufacturersInformations { get; set; } = null!;
		public virtual DbSet<Product> Products { get; set; } = null!;
		public virtual DbSet<ProductBalance> ProductBalances { get; set; } = null!;
		public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
		public virtual DbSet<Provaider> Provaiders { get; set; } = null!;
		public virtual DbSet<ProvidersInformation> ProvidersInformations { get; set; } = null!;
		public virtual DbSet<ReceiptReport> ReceiptReports { get; set; } = null!;
		public virtual DbSet<ReleaseReport> ReleaseReports { get; set; } = null!;
		public virtual DbSet<Storage> Storages { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder
					.UseLazyLoadingProxies()
					.UseSqlServer(DataBaseConnection.Instance.GetConnection());
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CustomerConfigurator());
			modelBuilder.ApplyConfiguration(new EmployerConfigurator());
			modelBuilder.ApplyConfiguration(new ManufacturerConfigurator());
			modelBuilder.ApplyConfiguration(new ProductConfigurator());
			modelBuilder.ApplyConfiguration(new ProductTypeConfigurator());
			modelBuilder.ApplyConfiguration(new ProvaiderConfigurator());
			modelBuilder.ApplyConfiguration(new ReceipReportConfigurator());
			modelBuilder.ApplyConfiguration(new ReleaseReportConfigurator());
			modelBuilder.ApplyConfiguration(new StorageConfigurator());

			modelBuilder.Entity<ManufacturersInformation>(entity =>
			{
				entity.HasNoKey();

				entity.ToView("ManufacturersInformation");

				entity.Property(e => e.ManufacturersName).HasMaxLength(50);

				entity.Property(e => e.ProductName).HasMaxLength(50);
			});

			modelBuilder.Entity<ProductBalance>(entity =>
			{
				entity.HasNoKey();

				entity.ToView("ProductBalance");

				entity.Property(e => e.ProductName).HasMaxLength(50);

				entity.Property(e => e.Storage).HasMaxLength(20);
			});

			modelBuilder.Entity<ProvidersInformation>(entity =>
			{
				entity.HasNoKey();

				entity.ToView("ProvidersInformation");

				entity.Property(e => e.ProductName).HasMaxLength(50);

				entity.Property(e => e.ProvidersName).HasMaxLength(20);

				entity.Property(e => e.ReceipDate).HasColumnType("date");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
