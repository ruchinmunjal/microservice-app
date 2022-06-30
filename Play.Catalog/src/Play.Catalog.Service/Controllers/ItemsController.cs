using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private static readonly IList<ItemsDto> Items{
        
    }

}