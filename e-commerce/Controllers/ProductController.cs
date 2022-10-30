using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet("index")]
    public string Index()
    {
        return "products";
    }

    [HttpGet("{id:long}")]
    public string Product(long id)
    {
        return "product";
    }
}