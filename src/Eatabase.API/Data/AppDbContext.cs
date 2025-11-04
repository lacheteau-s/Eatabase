using Eatabase.API.Features.Products;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.Data;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Product> Products { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
	}
}
