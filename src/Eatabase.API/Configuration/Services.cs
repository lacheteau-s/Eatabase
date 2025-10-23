using Eatabase.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.Configuration;

internal static class ServicesConfiguration
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		services.AddOpenApi();

		services.AddDbContext<AppDbContext>(options => options.UseSqlServer());
	}
}
