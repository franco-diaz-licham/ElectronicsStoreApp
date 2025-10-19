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
    [Reference] public BrandsRow? Brand { get; set; }
    [References(typeof(CategoriesRow))] public int CategoryId { get; set; }
    [Reference] public CategoriesRow? Category { get; set; }
    [References(typeof(SpecTypesRow))] public int SpecsTypeId { get; set; }
    [Reference] public SpecTypesRow? SpecTypes { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}