using Microsoft.AspNetCore.Mvc;
using Online.Models;
using Online.Repositories;
using Online.DTOs;
using Online.Repositories;

namespace Online.Controllers;

[ApiController]
[Route("api/Customer")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerRepository _Customer;
  //  private readonly IProductRepository _Product;
    private readonly IOrderRepository _order;

    public CustomerController(ILogger<CustomerController> logger,
    ICustomerRepository Customer, IProductRepository Product,
    IOrderRepository _order)
    {
        _logger = logger;
        _Customer = Customer;
       //// _Product = Product;
       this._order = _order;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetList()
    {
        var res = await _Customer.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var res = await _Customer.GetById(id);

        if (res == null)
            return NotFound();

        var dto = res.asDto;
       // dto.Products = (await _Product.GetListByCustomerId(id))
                        //.Select(x => x.asDto).ToList();
        dto.Orders = (await _order.GetListByCustomerId(id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CustomerDTO Data)
    {
        var toCreateCustomer = new Customer
        {
            CustomerName = Data.CustomerName?.Trim(),
            Address = Data.Address?.Trim(),
            MobileNumber = Data.MobileNumber
            
          
          
        };

        var res = await _Customer.Create(toCreateCustomer);

        return StatusCode(StatusCodes.Status201Created, res.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CustomerCreateDTO Data)
    {
        var existingCustomer = await _Customer.GetById(id);

        if (existingCustomer == null)
            return NotFound();

        var toUpdateCustomer = existingCustomer with
        {
           
            CustomerName = Data.CustomerName?.Trim(),
            Address = Data.Address?.Trim(),
            MobileNumber = Data.MobileNumber
        };

        var didUpdate = await _Customer.Update(toUpdateCustomer);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }
}
