using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private static readonly IList<ItemDto> Items = new List<ItemDto>()
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
    [HttpGet(Name = "GetAllItems")]
    public IEnumerable<ItemDto> GetAllItems()
    {
        return Items;
    }
    [HttpGet(Name = "GetById")]
    public ActionResult<ItemDto> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest("Id can not be empty");
        }

        return Items.Single(x => x.Id.ToString() == id);
    }
    [HttpGet(Name = "GetById")]
    public ActionResult<ItemDto> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Id can not be empty");
        }
        return Items.Single(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));
    }

}