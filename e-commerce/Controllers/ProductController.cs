using core.Interfaces;
using core.Models;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private IGenericRepository<Product> _repository;
    public ProductController(IGenericRepository<Product> repository)
    {
        _repository = repository;
    }

    [HttpGet("index")]
    public async Task<ActionResult<List<Product>>> Index()
    {
        var specification = new ProductsWithTypesAndBrandsSpecification().WithBrands().WithTypes();
        var products = await _repository.ListAsync(specification);

        return Ok(products);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Product>> Product(long id)
    {
        var specification = new ProductsWithTypesAndBrandsSpecification(x => x.Id == id)
            .WithBrands()
            .WithTypes();
        var product = await _repository.GetModelWithSpecification(specification);

        return Ok(product);
    }
}