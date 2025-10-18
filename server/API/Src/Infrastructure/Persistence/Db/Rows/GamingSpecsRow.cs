namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("gaming_specs")]
public class GamingSpecsRow
{
    [PrimaryKey][References(typeof(ProductsRow))] public int ProductId { get; set; }
    public string Resolution { get; set; } = "";
    public string Storage { get; set; } = "";
    public string? Battery { get; set; }
    public string Features { get; set; } = "";
    public string BackwardCompatibility { get; set; } = "";
}
