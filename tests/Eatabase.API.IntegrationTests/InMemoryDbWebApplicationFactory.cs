using Eatabase.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Eatabase.API.IntegrationTests;

public class InMemoryDbWebApplicationFactory : WebApplicationFactory<Program>
{
	// New scope => new instance of DbContext => new DbContextOptions initialization
	// UseInMemoryDatabase(Guid.NewGuid().ToString()) => new database created for each scope
	// Use a fixed name to prevent getting a different database for each scope
	private readonly string _databaseName = Guid.NewGuid().ToString();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureTestServices(services =>
		{
			// Use IDbContextOptionsConfiguration instead of DbContextOptions
			// https://github.com/dotnet/efcore/issues/35126
			var serviceType = typeof(IDbContextOptionsConfiguration<AppDbContext>);
			var descriptor = services.SingleOrDefault(x => x.ServiceType == serviceType);

			if (descriptor is not null)
				services.Remove(descriptor);

			services.AddDbContext<AppDbContext>(options =>
				options.UseInMemoryDatabase(_databaseName)
			);

			using var scope = services.BuildServiceProvider().CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

			dbContext.Database.EnsureCreated();
		});
	}
}
