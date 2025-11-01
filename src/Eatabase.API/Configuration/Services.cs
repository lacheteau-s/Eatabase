using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.Configuration;

internal static class ServicesConfiguration
{
	public static void ConfigureServices(this IHostApplicationBuilder builder)
	{
		var services = builder.Services;
		var configuration = builder.Configuration;

		services.AddOpenApi();

		services.AddDbContextWithConnectionString<AppDbContext>(configuration.GetConnectionString("Eatabase"));

		services.AddScoped<CreateProductRequestHandler>();
		services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
	}

	private static void AddDbContextWithConnectionString<T>(
		this IServiceCollection services,
		string? connectionString
	) where T : DbContext
	{
		if (string.IsNullOrWhiteSpace(connectionString))
			throw new InvalidOperationException($"Connection string '{connectionString}' not found.");

		services.AddDbContext<T>(options => options.UseSqlServer(connectionString));
	}
}
