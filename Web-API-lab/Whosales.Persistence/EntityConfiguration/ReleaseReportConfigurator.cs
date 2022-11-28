using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whosales.Domain;

namespace Whosales.Persistence.EntityConfiguration
{
	internal class ReleaseReportConfigurator : IEntityTypeConfiguration<ReleaseReport>
	{
		public void Configure(EntityTypeBuilder<ReleaseReport> builder)
		{
			builder.Property(e => e.OrderDate).HasColumnType("date");

			builder.Property(e => e.ReciveDate).HasColumnType("date");

			builder.Property(e => e.ReleaseDate).HasColumnType("date");

			builder.HasOne(d => d.Customer)
				.WithMany(p => p.ReleaseReports)
				.HasForeignKey(d => d.CustomerId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ReleaseReports_To_Customers");

			builder.HasOne(d => d.Employer)
				.WithMany(p => p.ReleaseReports)
				.HasForeignKey(d => d.EmployerId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ReleaseReports_To_Employers");

			builder.HasOne(d => d.Product)
				.WithMany(p => p.ReleaseReports)
				.HasForeignKey(d => d.ProductId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ReleaseReports_To_Products");

			builder.HasOne(d => d.Storage)
				.WithMany(p => p.ReleaseReports)
				.HasForeignKey(d => d.StorageId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ReleaseReports_To_Storages");
		}
	}
}
