namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("products")]
public class ProductsRow
{
    [PrimaryKey] public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Details { get; set; } = "";
    public decimal Price { get; set; }
    [References(typeof(BrandsRow))] public int BrandId { get; set; }
    [References(typeof(CategoriesRow))] public int CategoryId { get; set; }
    [References(typeof(SpecsTypeRow))]  public int SpecsTypeId { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedOnUTC { get; set; }
}