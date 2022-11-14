using AutoMapper;
using core.Models;
using e_commerce.Dto;

namespace e_commerce.Helpers;

public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private IConfiguration _configuration;
    
    public ProductUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _configuration["AppUrl"] + source.PictureUrl;
        }

        return null!;
    }
}