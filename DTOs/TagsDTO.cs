
using System.Text.Json.Serialization;
using Online.Models;
  namespace Online.DTOs;


public record TagsDTO
{
    [JsonPropertyName("TagId")]
    public int TagId { get; set; }
     [JsonPropertyName("TagName")]
    public string TagName { get; set; }
     [JsonPropertyName("Price")]
    public double Price { get; set; }
     [JsonPropertyName("OrderId")]
    public int OrderId { get; set; }
     [JsonPropertyName("ProductId")]
    public int  ProductId { get; set; }
}

public record TagsCreateDTO
{
    
     [JsonPropertyName("TagName")]
    public string TagName { get; set; }
     [JsonPropertyName("Price")]
    public double Price { get; set; }
    
}
