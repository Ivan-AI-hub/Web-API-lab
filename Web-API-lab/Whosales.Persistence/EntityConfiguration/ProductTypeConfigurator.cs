using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whosales.Domain;

namespace Whosales.Persistence.EntityConfiguration
{
	internal class ProductTypeConfigurator : IEntityTypeConfiguration<ProductType>
	{
		public void Configure(EntityTypeBuilder<ProductType> builder)
		{
			builder.Property(e => e.Description).HasMaxLength(50);

			builder.Property(e => e.Feature).HasMaxLength(20);

			builder.Property(e => e.Name).HasMaxLength(50);
		}
	}
}
