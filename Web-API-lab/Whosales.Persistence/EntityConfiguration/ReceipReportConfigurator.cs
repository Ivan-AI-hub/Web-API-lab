using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Whosales.Domain;

namespace Whosales.Persistence.EntityConfiguration
{
	internal class ReceipReportConfigurator : IEntityTypeConfiguration<ReceiptReport>
	{
		public void Configure(EntityTypeBuilder<ReceiptReport> builder)
		{
			builder.Property(e => e.DepartureDate).HasColumnType("date");

			builder.Property(e => e.OrderDate).HasColumnType("date");

			builder.Property(e => e.ReciveDate).HasColumnType("date");

			builder.HasOne(d => d.Employer)
				.WithMany(p => p.ReceiptReports)
				.HasForeignKey(d => d.EmployerId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ReceiptReports_To_Employers");

			builder.HasOne(d => d.Product)
				.WithMany(p => p.ReceiptReports)
				.HasForeignKey(d => d.ProductId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ReceiptReports_To_Products");

			builder.HasOne(d => d.Provaider)
				.WithMany(p => p.ReceiptReports)
				.HasForeignKey(d => d.ProvaiderId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ReceiptReports_To_Provaiders");

			builder.HasOne(d => d.Storage)
				.WithMany(p => p.ReceiptReports)
				.HasForeignKey(d => d.StorageId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_ReceiptReports_To_Storages");
		}
	}
}
