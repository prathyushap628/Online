

using Online.DTOs;

namespace Online.Models;

public enum Gender
{
    Female = 1,
    Male = 2,
}

public record Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public long MobileNumber { get; set; }
   

    public CustomerDTO asDto => new CustomerDTO
    {
         CustomerName = CustomerName,
        Address = Address,
        MobileNumber = MobileNumber,
        
    };
}