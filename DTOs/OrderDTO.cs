using System.Text.Json.Serialization;
using Online.Models;
namespace Online.DTOs;



public record OrderDTO
{
    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
   [JsonPropertyName("customer_id")]
    public int CustomerId { get; set; }
    // [JsonPropertyName("products")]
   // public List<ProductsDTO> Products { get; set; }

   
}
public record OrderCreateDTO
{
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
  
   
}