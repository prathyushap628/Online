using Microsoft.AspNetCore.Mvc;
using Online.Models;
using Online.Repositories;
using Online.DTOs;

namespace Online.Controllers;

[ApiController]
[Route("api/Product")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _Product;
    private readonly ITagsRepository _tags;

    public ProductController(ILogger<ProductController> logger, IProductRepository Product, ITagsRepository tags)
    {
        _logger = logger;
        _Product = Product;
        _tags = tags;
    }

    [HttpGet]
    public async Task<ActionResult> GetList()
    {
        var res = await _Product.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var res = await _Product.GetById(id);

        if (res == null)
            return NotFound();

        var dto = res.asDto;
        // dto.Products = (await _Product.GetListByGuestId(id))
        //  .Select(x => x.asDto).ToList();
       dto.Tags = (await _tags.GetProductByTagId(id)).Select(x => x.asDto).ToList();

        return Ok(dto);

    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProductsDTO Data)
    {
        var toCreateProduct = new Products
        {
            ProductName = Data.ProductName?.Trim(),
            Price = Data.Price
           

        };

        var res = await _Product.Create(toCreateProduct);

        return StatusCode(StatusCodes.Status201Created, res.asDto);

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProductsCreateDTO Data)
    {
        var existingProduct = await _Product.GetById(id);

        if (existingProduct == null)
            return NotFound();

        var toUpdateProduct = existingProduct with
        {
          ProductName = Data.ProductName?.Trim(),
          
        };

        var didUpdate = await _Product.Update(toUpdateProduct);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var existing = await _Product.GetById(id);
        if (existing is null)
            return NotFound("No Product found with Product id");

        var didDelete = await _Product.Delete(id);

        return NoContent();

    }

}



