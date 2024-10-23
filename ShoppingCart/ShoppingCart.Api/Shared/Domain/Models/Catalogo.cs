namespace ShoppingCart.Api.Shared.Domain.Models;

public class Catalogo
{
  public string? Id { get; set; }
  public string? Name { get; set; }
  public string? Code { get; set; }
  public string? Description { get; set; }
  public decimal? Price { get; set; }
  public string? ImageUrl { get; set; }
  public Guid CategoryId { get; set; }
}
