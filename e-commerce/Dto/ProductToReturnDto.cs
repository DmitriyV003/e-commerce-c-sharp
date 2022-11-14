namespace e_commerce.Dto;

public class ProductToReturnDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }
    public ProductTypeToReturnDto ProductType { get; set; }
    public ProductBrandToReturnDto ProductBrand { get; set; }
}