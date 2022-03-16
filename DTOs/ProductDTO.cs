using System.Text.Json.Serialization;
using Online.Models;

namespace Online.DTOs;

public record ProductsDTO
{
   [JsonPropertyName("ProductId")]
    public int ProductId { get; set; }
     [JsonPropertyName("ProductName")]
    public string ProductName { get; set; }
     [JsonPropertyName("Price")]
    public double Price { get; set; }
       [JsonPropertyName("CustomerId")]
    public int CustomerId { get; set; }
      [JsonPropertyName("TagId")]
    public int TagId { get; set; }
     [JsonPropertyName("Tags")]
    public List<TagsDTO> Tags { get; set; }
}

public record ProductsCreateDTO
{
   
     [JsonPropertyName("ProductName")]
    public string ProductName { get; set; }
  
 
}
