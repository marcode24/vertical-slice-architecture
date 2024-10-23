using ShoppingCart.Api.Features.Catalogo;
using ShoppingCart.Api.Shared.Networking.CatalogoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICatalogoApiClient, CatalogoApiClient>();

builder.Services.AddHttpClient<CatalogoApiService>((serviceProvider, httpClient) =>
{
  httpClient.BaseAddress = new Uri("http://localhost:5000");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
  return new SocketsHttpHandler
  {
    PooledConnectionLifetime = TimeSpan.FromMinutes(5)
  };
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

GetCatalogo.AddEndpoint(app);

app.Run();
