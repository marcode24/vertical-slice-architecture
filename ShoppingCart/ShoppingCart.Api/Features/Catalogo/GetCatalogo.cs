using ShoppingCart.Api.Shared.Networking.CatalogoApi;

namespace ShoppingCart.Api.Features.Catalogo;

public static class GetCatalogo
{
  public static void AddEndpoint(IEndpointRouteBuilder app)
  {
    app.MapGet("api/catalogo", async (
      ILoggerFactory loggerFactory,
      ICatalogoApiClient catalogoClient,
      CancellationToken cancellationToken) =>
    {
      loggerFactory
        .CreateLogger("EndpointCatalogo-Get")
        .LogInformation("");

      var result = await catalogoClient.GetProductsAsync(cancellationToken);
      return Results.Ok(result);
    });
  }
}
