namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("products")]
public class ProductRow
{
    [PrimaryKey] public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Details { get; set; } = "";
    public decimal Price { get; set; }
    [Alias("brand_id"), References(typeof(BrandRow))] public int BrandId { get; set; }
    [Alias("category_id"), References(typeof(CategoryRow))] public int CategoryId { get; set; }
    [Alias("specs_type_id"), References(typeof(SpecsTypeRow))]  public int SpecsTypeId { get; set; }
    public int Stock { get; set; }
    [Alias("created_on_utc")] public DateTime CreatedOnUTC { get; set; }
}