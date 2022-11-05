using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models;

[Table("products")]
public class Product : Base
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("picture_url")]
    public string PictureUrl { get; set; }
    
    public ProductType ProductType { get; set; }
    [Column("product_type_id")]
    public long ProductTypeId { get; set; }
    
    public ProductBrand ProductBrand { get; set; }
    [Column("product_brand_id")]
    public long ProductBrandId { get; set; }
}