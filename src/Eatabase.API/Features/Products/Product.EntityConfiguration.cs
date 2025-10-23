using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eatabase.API.Features.Products;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
	void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("Products").HasKey(p => p.Id);

		builder.Property(p => p.Id).ValueGeneratedOnAdd();

		builder.Property(p => p.CreatedAt).IsRequired();

		builder.Property(p => p.UpdatedAt);

		builder.Property(p => p.Brand).IsRequired().HasMaxLength(50);

		builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

		builder.Property(p => p.ServingSize).IsRequired().HasMaxLength(25);

		builder.Property(p => p.ServingSizeMetric).IsRequired().HasMaxLength(25);

		builder.Property(p => p.Calories).IsRequired();

		builder.Property(p => p.TotalFat).IsRequired().HasPrecision(3, 1);

		builder.Property(p => p.SaturatedFat).HasPrecision(3, 1);

		builder.Property(p => p.TransFat).HasPrecision(3, 1);

		builder.Property(p => p.TotalCarbs).IsRequired().HasPrecision(3, 1);

		builder.Property(p => p.Sugars).HasPrecision(3, 1);

		builder.Property(p => p.Fiber).HasPrecision(3, 1);

		builder.Property(p => p.Protein).IsRequired().HasPrecision(3, 1);
	}
}
