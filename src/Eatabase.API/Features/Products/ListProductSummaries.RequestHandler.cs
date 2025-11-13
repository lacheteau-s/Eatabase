using Eatabase.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.Features.Products;

internal sealed class ListProductSummariesRequestHandler(AppDbContext dbContext)
{
	public async Task<List<ProductSummary>> HandleAsync(CancellationToken ct)
	{
		var summaries = await dbContext.Products
			.AsNoTracking()
			.Select(p => new ProductSummary(
				p.Id,
				p.Brand,
				p.Name
			)).ToListAsync(ct);

		return summaries;
	}
}
