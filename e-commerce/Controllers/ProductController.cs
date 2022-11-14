using AutoMapper;
using core.Interfaces;
using core.Models;
using core.Specifications;
using e_commerce.Dto;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private IGenericRepository<Product> _repository;
    private IMapper _mapper;
    public ProductController(IGenericRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("index")]
    public async Task<ActionResult<IReadOnlyList<Product>>> Index()
    {
        var specification = new ProductsWithTypesAndBrandsSpecification().WithBrands().WithTypes();
        var products = await _repository.ListAsync(specification);

        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProductToReturnDto>> Product(long id)
    {
        var specification = new ProductsWithTypesAndBrandsSpecification(x => x.Id == id)
            .WithBrands()
            .WithTypes();
        var product = await _repository.GetModelWithSpecification(specification);
        if (product != null)
        {
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        return NotFound();
    }
}