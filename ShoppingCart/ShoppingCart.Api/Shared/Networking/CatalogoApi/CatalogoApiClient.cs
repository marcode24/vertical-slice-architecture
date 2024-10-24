using Polly;
using Polly.Registry;
using ShoppingCart.Api.Shared.Domain.Models;

namespace ShoppingCart.Api.Shared.Networking.CatalogoApi;

public sealed class CatalogoApiClient(
  CatalogoApiService catalogoApiService,
  ILoggerFactory loggerFactory,
  ResiliencePipelineProvider<string> pipelineProvider)
  : ICatalogoApiClient
{

  private readonly CatalogoApiService _catalogoApiService = catalogoApiService;
  private readonly ILoggerFactory _loggerFactory = loggerFactory;
  private readonly ResiliencePipelineProvider<string> _pipelineProvider = pipelineProvider;

  public async Task<Catalogo?> GetProductByCodeAsync(string code, CancellationToken cancellationToken)
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

    var product = await policy.ExecuteAsync(()
      => _catalogoApiService.GetProductByCode(code, cancellationToken));

    return product;
  }

  public async Task<IEnumerable<Catalogo>> GetProductsAsync(CancellationToken cancellationToken)
  {
    var logger = _loggerFactory.CreateLogger("RetryLog");
    // var policy = Policy.Handle<ApplicationException>()
    //   .WaitAndRetryAsync(
    //     3,
    //     retryAttempt =>
    //     {
    //       logger.LogInformation($"Retry count: {retryAttempt}");
    //       var timeToRetry = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
    //       return timeToRetry;
    //     });

    // var products = await policy.ExecuteAsync(()
    //   => _catalogoApiService.GetProductsAsync(cancellationToken));

    var pipeline = _pipelineProvider.GetPipeline<IEnumerable<Catalogo>>("catalogo-products");
    var products = await pipeline
      .ExecuteAsync(async token => await _catalogoApiService.GetProductsAsync(token), cancellationToken);

    return products;
  }
}
