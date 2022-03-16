using Microsoft.AspNetCore.Mvc;
using Online.Models;
using Online.Repositories;
using Online.DTOs;

namespace Online.Controllers;

[ApiController]
[Route("api/Order")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderRepository _order;
      private readonly IProductRepository _product;

    public OrderController(ILogger<OrderController> logger, IOrderRepository _order, IProductRepository product)
    {
        _logger = logger;
        this._order = _order;
        _product = product;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetList()
    {
        var res = await _order.GetList();

        return Ok(res.Select(x => x.asDto));
    }



    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var res = await _order.GetById(id);

        if (res is null)
            return NotFound();

            var dto = res.asDto;
           //  dto.Products = (await _product.GetProductByOrderId(id)).Select(x => x.asDto).ToList();
            //  dto.Products = (await _product.GetProductByOrderId(id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

   

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] OrderCreateDTO Data)
    {
        var existingOrder = await _order.GetById(id);

        if (existingOrder == null)
            return NotFound();

        var toUpdateOrder = existingOrder with
        {
        
            Status = Data.Status?.Trim(),

        };

        var didUpdate = await _order.Update(toUpdateOrder);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }


     [HttpDelete("{id}")]
     public async Task<ActionResult> Delete([FromRoute] int id)
     {
       var existing = await _order.GetById(id);
        if (existing is null)
            return NotFound("No order found with order id"); 

            var didDelete = await _order.Delete(id);

            return NoContent();
    
    }

}