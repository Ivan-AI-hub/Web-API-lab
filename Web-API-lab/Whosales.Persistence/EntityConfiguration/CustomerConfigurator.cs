
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whosales.Domain;

namespace Whosales.Persistence.EntityConfiguration
{
	internal class CustomerConfigurator : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.Property(e => e.Address).HasMaxLength(20);
			builder.Property(e => e.Name).HasMaxLength(20);
			builder.Property(e => e.TelephoneNumber).HasMaxLength(20);
		}
	}
}
