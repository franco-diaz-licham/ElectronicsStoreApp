namespace API.Src.Infrastructure.Persistence.Db.Rows;

public class GamingSpecsRow
{
    [PrimaryKey][Alias("product_id"), References(typeof(ProductRow))] public int ProductId { get; set; }
    public string Resolution { get; set; } = "";
    public string Storage { get; set; } = "";
    public string? Battery { get; set; }
    public string Features { get; set; } = "";
    [Alias("backward_compatability")] public string BackwardCompatibility { get; set; } = "";
}
