using Eatabase.API.Features.Products;

namespace Eatabase.API.Configuration;

internal static class EndpointsConfiguration
{
	public static void ConfigureEndpoints(this IEndpointRouteBuilder router)
	{
		CreateProduct.Register(router);
	}
}
