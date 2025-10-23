namespace Eatabase.API.Configuration;

internal static class ServicesConfiguration
{
	public static void ConfigureServices(this IServiceCollection services)
	{
		services.AddOpenApi();
	}
}
