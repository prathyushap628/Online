using System.Text.Json.Serialization;
using Online.Models;

namespace Online.DTOs;

public record CustomerDTO

{
    [JsonPropertyName("customer_id")]
    public int CustomerId { get; set; }
     [JsonPropertyName("customer_name")]
    public string CustomerName { get; set; }
     [JsonPropertyName("Address")]
    public string Address { get; set; }

     [JsonPropertyName("mobile_number")]
    public long MobileNumber { get; set; }
      [JsonPropertyName("orders")]
    public List<OrderDTO> Orders { get; set; }
}

public record CustomerCreateDTO

{

    [JsonPropertyName("customer_name")]
    public string CustomerName { get; set; }
     [JsonPropertyName("Address")]
    public string Address { get; set; }

     [JsonPropertyName("mobile_number")]
    public long MobileNumber { get; set; }
}