using AutoMapper;
using core.Models;
using e_commerce.Dto;

namespace e_commerce.Helpers;

public class MappingProfiles : Profile
{
    protected MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        CreateMap<ProductType, ProductTypeToReturnDto>();
        CreateMap<ProductBrand, ProductBrandToReturnDto>();
    }
}