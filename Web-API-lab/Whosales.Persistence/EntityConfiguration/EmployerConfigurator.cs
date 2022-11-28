using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whosales.Domain;

namespace Whosales.Persistence.EntityConfiguration
{
	internal class EmployerConfigurator : IEntityTypeConfiguration<Employer>
	{
		public void Configure(EntityTypeBuilder<Employer> builder)
		{
			builder.Property(e => e.Name).HasMaxLength(20);
		}
	}
}
