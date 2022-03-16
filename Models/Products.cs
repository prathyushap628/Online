using Online.DTOs;

namespace Online.Models;

public record Products
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public int CustomerId { get; set; }
    public int TagId { get; set; }
  
    


    public ProductsDTO asDto => new ProductsDTO
    {
        ProductName = ProductName,
        Price = Price,
    };
}