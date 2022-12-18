namespace core.Specifications;

public class ProductsSpecificationParams
{
    private const int MaxPerPage = 50;
    
    public string Sort { get; set; }
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public int Page { get; set; } = 1;

    private int _perPage = 5;
    private string _search;

    public string Search
    {
        get => _search;
        set => _search = value.ToLower();
    }

    public int PerPage
    {
        get => _perPage;
        set => _perPage = (value > MaxPerPage) ? MaxPerPage : value;
    }
}