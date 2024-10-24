using ShoppingCart.Api.Shared.Networking.CatalogoApi;

namespace ShoppingCart.Api.Features.Catalogo;

public static class SearchProduct
{
  public static void AddEndpoint(IEndpointRouteBuilder app)
  {
    app.MapGet("api/catalogo/{code}", async (
      string code,
      ILoggerFactory loggerFactory,
      ICatalogoApiClient catalogoClient,
      CancellationToken cancellationToken
    ) =>
    {
      loggerFactory
        .CreateLogger("EndpointCatalogo-Code")
        .LogInformation("Search product by code");

      var result = await catalogoClient.GetProductByCodeAsync(code, cancellationToken);

      return Results.Ok(result);
    });
  }
}
