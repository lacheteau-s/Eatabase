namespace Eatabase.API.Configuration;

internal static class MiddlewaresConfiguration
{
	public static void ConfigureMiddlewares(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
		}

		app.UseHttpsRedirection();
	}
}
