using System;
namespace Play.Catalog.Service.Dtos;

public record ItemDto(Guid Id,string Name, string Description, decimal Price, DateTimeOffset DateCreated);
public record CreateItemDto(string Name, string Description, decimal Price);

