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
        if (id != Guid.Empty) return Items.Single(x => x.Id == id);
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

}