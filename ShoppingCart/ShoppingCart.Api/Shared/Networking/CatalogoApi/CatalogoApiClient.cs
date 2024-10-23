using Polly;
using ShoppingCart.Api.Shared.Domain.Models;

namespace ShoppingCart.Api.Shared.Networking.CatalogoApi;

public sealed class CatalogoApiClient(
  CatalogoApiService catalogoApiService,
  ILoggerFactory loggerFactory)
  : ICatalogoApiClient
{

  private readonly CatalogoApiService _catalogoApiService = catalogoApiService;
  private readonly ILoggerFactory _loggerFactory = loggerFactory;

  public Task<Catalogo> GetProductByCodeAsync(string code, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Catalogo>> GetProductsAsync(CancellationToken cancellationToken)
  {
    var logger = _loggerFactory.CreateLogger("RetryLog");
    var policy = Policy.Handle<ApplicationException>()
      .WaitAndRetryAsync(
        3,
        retryAttempt =>
        {
          logger.LogInformation($"Retry count: {retryAttempt}");
          var timeToRetry = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
          return timeToRetry;
        });

    var products = await policy.ExecuteAsync(()
      => _catalogoApiService.GetProductsAsync(cancellationToken));

    return products;
  }
}
