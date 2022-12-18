using System.Net;
using AutoMapper;
using core.Interfaces;
using core.Models;
using core.Specifications;
using e_commerce.Dto;
using e_commerce.Errors;
using e_commerce.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace e_commerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseController
{
    private IGenericRepository<Product> _repository;
    private IMapper _mapper;
    public ProductController(IGenericRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IReadOnlyList<ProductToReturnDto>), contentTypes: new []{"application/json"})]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> Index([FromQuery] ProductsSpecificationParams productParams)
    {
        var specification = new ProductsWithTypesAndBrandsSpecification(productParams).WithBrands().WithTypes();
        var countSpecification = new ProductWithFiltersForCountSpecification(productParams);
        var totalItems = await _repository.CountAsync(countSpecification);
        var products = await _repository.ListAsync(specification);

        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

        return Ok(new Pagination<ProductToReturnDto>(productParams.Page, productParams.PerPage, totalItems, data));
    }

    [HttpGet("{id:long}")]
    [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(ApiResponse), contentTypes: new []{"application/json"})]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ProductToReturnDto), contentTypes: new []{"application/json"})]
    public async Task<ActionResult<ProductToReturnDto>> Product(long id)
    {
        var specification = new ProductsWithTypesAndBrandsSpecification(x => x.Id == id)
            .WithBrands()
            .WithTypes();
        var product = await _repository.GetModelWithSpecification(specification);
        
        if (product == null) return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));

        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }
}