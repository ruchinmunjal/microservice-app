using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private static readonly List<ItemDto> Items = new()
    {
        new ItemDto(Guid.NewGuid(),"Item1","Sample Description for Item1", 2.0M,DateTimeOffset.Now),
        new ItemDto(Guid.NewGuid(),"Item2","Sample Description for Item2", 12.0M,DateTimeOffset.Now),
        new ItemDto(Guid.NewGuid(),"Item3","Sample Description for Item3", 21.0M,DateTimeOffset.Now),
        new ItemDto(Guid.NewGuid(),"Item4","Sample Description for Item4", 8.0M,DateTimeOffset.Now),
    };

    private readonly ILogger<ItemsController> _logger;

    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IEnumerable<ItemDto> GetAllItems()
    {
        return Items;
    }
    [HttpGet("{id:guid}")]
    public ActionResult<ItemDto> GetById(Guid id)
    {
        if (id != Guid.Empty)
        {
            var item=Items.SingleOrDefault(x => x.Id == id);
            if (item!=null)
            {
                return item;
            }
            _logger.LogWarning("Bad Id:{Id} received",id);
            return NotFound("Given id nt found");
        }
        _logger.LogWarning("Empty guid id received");
        return BadRequest("Id can not be empty");

    }
    [HttpGet("{name}")]
    public ActionResult<ItemDto> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Id can not be empty");
        }
        return Items.Single(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));
    }

    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto item)
    {
        var newItem = new ItemDto(Guid.NewGuid(), item.Name,item.Description,item.Price,DateTimeOffset.Now);
        Items.Add(newItem);
        return CreatedAtAction(nameof(GetById),new {id=newItem.Id},newItem);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateItem(Guid id, UpdateItemDto item)
    {
        var existingItem = Items.Single(x => x.Id == id);
        var updateItem = existingItem with
        {
            Name = item.Name,
            Description = item.Description,
            Price = item.Price
        };
        var index = Items.FindIndex(x => x.Id == id);
        Items[index] = updateItem;
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteItem(Guid id)
    {
        var index = Items.FindIndex(x => x.Id == id);
        Items.RemoveAt(index);
        return NoContent();
    }

}