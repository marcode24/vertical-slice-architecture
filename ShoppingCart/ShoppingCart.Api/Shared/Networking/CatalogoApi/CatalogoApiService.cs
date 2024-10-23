using ShoppingCart.Api.Shared.Domain.Models;

namespace ShoppingCart.Api.Shared.Networking.CatalogoApi;

public sealed class CatalogoApiService(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;

  public async Task<IEnumerable<Catalogo>> GetProductsAsync(
    CancellationToken cancellationToken)
  {
    var content = await _httpClient.GetFromJsonAsync<IEnumerable<Catalogo>>(
      "/api/products",
      cancellationToken
    );

    return content ?? Enumerable.Empty<Catalogo>();
  }
}
