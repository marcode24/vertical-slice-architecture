using ShoppingCart.Api.Shared.Domain.Models;

namespace ShoppingCart.Api.Shared.Networking.CatalogoApi;

public sealed class CatalogoApiService(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;

  private readonly Random random = new Random();

  public async Task<IEnumerable<Catalogo>> GetProductsAsync(
    CancellationToken cancellationToken)
  {

    if (random.NextDouble() < 0.9)
      throw new ApplicationException("Error fetching products");

    var content = await _httpClient.GetFromJsonAsync<IEnumerable<Catalogo>>(
      "/api/products",
      cancellationToken
    );

    return content ?? Enumerable.Empty<Catalogo>();
  }

  public async Task<Catalogo?> GetProductByCode(
    string code,
    CancellationToken cancellationToken
  )
  {

    if (random.NextDouble() < 0.8)
      throw new ApplicationException("Error fetching product");

    var content = await _httpClient.GetFromJsonAsync<Catalogo>(
      $"api/products/code/{code}",
      cancellationToken
    );

    return content;
  }
}
