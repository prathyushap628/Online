using Online.DTOs;

namespace Online.Models;


public record Tags
{
    public int TagId { get; set; }
    public string TagName { get; set; }
    public double Price { get; set; }
  
    public int OrderId { get; set; }
    public int  ProductId { get; set; }




    public TagsDTO asDto => new TagsDTO
    {
        TagName = TagName,
        Price = Price,
    };
}