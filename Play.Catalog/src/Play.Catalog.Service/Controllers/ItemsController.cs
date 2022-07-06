using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ItemsRepository _itemsRepository = new();

    private readonly ILogger<ItemsController> _logger;

    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public async Task<List<ItemDto>> GetAllItems()
    {
        var items = await _itemsRepository.GetALlAsync();
        return items.Select(item => item.ToDto()).ToList();
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemDto>> GetById(Guid id)
    {
        if (id != Guid.Empty)
        {
            var item =  await _itemsRepository.GetByIdAsync(id);
            
            //_logger.LogWarning("Bad Id:{Id} received",id);
            return (ActionResult<ItemDto>) item.ToDto() ?? NotFound("Given id not found");
        }
        _logger.LogWarning("Empty guid id received");
        return BadRequest("Id can not be empty");

    }
    

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto item)
    {
        var newItem = new ItemDto(Guid.NewGuid(), item.Name,item.Description,item.Price,DateTimeOffset.Now);
        await _itemsRepository.CreateAsync(newItem.ToEntity());
        return CreatedAtAction(nameof(GetById),new {id=newItem.Id},newItem);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateItem(Guid id, UpdateItemDto item)
    {
        var existingItem = await _itemsRepository.GetByIdAsync(id);
        if (existingItem==null)
        {
            _logger.LogWarning("UpdateItem:Id:{Id} not found",id);
            return NotFound("Item with the given Id not found");
        }
        existingItem.Description = item.Description;
        existingItem.Name = item.Name;
        existingItem.Price = item.Price;

        await _itemsRepository.UpdateAsync(existingItem);
        
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        var item = await _itemsRepository.GetByIdAsync(id);
        if (item==null)
        {
            _logger.LogWarning("DeleteItem: Item with Id:{Id} not found",id);
            return NotFound("Item with the given Id not found");
        }
        await _itemsRepository.RemoveAsync(id);
        return NoContent();
    }

}