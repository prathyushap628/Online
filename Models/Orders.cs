using Online.DTOs;
namespace Online.Models;



public record Orders
{
    public int OrderId { get; set; }
   
    public string Status { get; set; }
   
    public int CustomerId { get; set; }
   


    public OrderDTO asDto => new OrderDTO
    {
        Status = Status.ToString(),
       
    };

    
}