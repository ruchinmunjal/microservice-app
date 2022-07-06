using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service;

public static class Extensions
{
    public static ItemDto ToDto(this Item item)
    {
        return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.DateCreated);
    }

    public static Item ToEntity(this ItemDto itemDto)
    {
        return new Item
        {
            Description = itemDto.Description,
            Name = itemDto.Name,
            Price = itemDto.Price,
            Id = itemDto.Id,
            DateCreated = itemDto.DateCreated

        };
    }
}