using Microsoft.AspNetCore.Mvc;
using Online.DTOs;
using Online.Models;
using Online.Repositories;

namespace Online.Controllers;

[ApiController]
[Route("api/Tags")]
public class TagsController : ControllerBase
{
    private readonly ILogger<TagsController> _logger;

    private readonly ITagsRepository _Tags;

    // private readonly IScheduleRepository _schedule;
    // private readonly IRoomRepository _room;


    public TagsController(ILogger<TagsController> logger, ITagsRepository Tags)
    {
        _logger = logger;
        _Tags = Tags;
        //  _schedule = schedule;
        // this._room = _room;
    }

    [HttpGet]
    public async Task<ActionResult<List<TagsDTO>>> GetList()
    {
        var res = await _Tags.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var res = await _Tags.GetById(id);

        if (res == null)
            return NotFound();

        var dto = res.asDto;
        // dto.Schedules = (await _schedule.GetListByTagsId(id)).asDto;


        //  dto.Rooms = (await _room.GetListByTagsId(id)).asDto;

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TagsDTO Data)
    {
        var toCreateTags = new Tags
        {
            TagId = Data.TagId,
            TagName = Data.TagName?.Trim(),
            Price = Data.Price,
            OrderId = Data.OrderId,
            ProductId = Data.ProductId

        };

        var res = await _Tags.Create(toCreateTags);

        return StatusCode(StatusCodes.Status201Created, res.asDto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] TagsCreateDTO Data)
    {
        var existingTag = await _Tags.GetById(id);

        if (existingTag == null)
            return NotFound();

        var toUpdateTag = existingTag with
        {
            TagName = Data.TagName?.Trim(),
            Price = Data.Price
        };

        var didUpdate = await _Tags.Update(toUpdateTag);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }

}

