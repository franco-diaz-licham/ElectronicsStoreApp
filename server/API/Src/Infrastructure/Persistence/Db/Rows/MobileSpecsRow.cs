namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("mobile_specs")]
public class MobileSpecsRow
{
    [PrimaryKey] [Alias("product_id"), References(typeof(ProductRow))] public int ProductId { get; set; }
    public string ScreenSize { get; set; } = "";
    public string Storage { get; set; } = "";
    public string Battery { get; set; } = "";
    public string Connectivity { get; set; } = "";
    [Alias("water_resistance")] public string WaterResistance { get; set; } = "";
}
