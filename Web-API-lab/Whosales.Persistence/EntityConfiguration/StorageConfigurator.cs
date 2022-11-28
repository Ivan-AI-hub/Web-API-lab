using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whosales.Domain;

namespace Whosales.Persistence.EntityConfiguration
{
	internal class StorageConfigurator : IEntityTypeConfiguration<Storage>
	{
		public void Configure(EntityTypeBuilder<Storage> builder)
		{
			builder.Property(e => e.Name).HasMaxLength(20);
		}
	}
}
