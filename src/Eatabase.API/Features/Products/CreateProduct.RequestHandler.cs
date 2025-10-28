namespace Eatabase.API.Features.Products;

internal class CreateProductRequestHandler
{
	public Guid Handle(CreateProductRequest request)
	{
		return Guid.NewGuid();
	}
}
