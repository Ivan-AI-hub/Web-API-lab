using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whosales.Domain;

namespace Whosales.Persistence.EntityConfiguration
{
	internal class ProductConfigurator : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(e => e.Name).HasMaxLength(50);

			builder.Property(e => e.Package).HasMaxLength(20);

			builder.Property(e => e.StorageConditions).HasMaxLength(30);

			builder.Property(e => e.StorageLife).HasColumnType("date");

			builder.HasOne(d => d.Manufacturer)
				.WithMany(p => p.Products)
				.HasForeignKey(d => d.ManufacturerId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Products_To_Manufacturers");

			builder.HasOne(d => d.Type)
				.WithMany(p => p.Products)
				.HasForeignKey(d => d.TypeId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Products_To_ProductTypes");
		}
	}
}
