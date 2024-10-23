using ShoppingCart.Api.Shared.Domain.Models;

namespace ShoppingCart.Api.Shared.Networking.CatalogoApi;

public sealed class CatalogoApiClient
  : ICatalogoApiClient
{
  public Task<Catalogo> GetProductByCodeAsync(string code, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Catalogo>> GetProductsAsync(CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
