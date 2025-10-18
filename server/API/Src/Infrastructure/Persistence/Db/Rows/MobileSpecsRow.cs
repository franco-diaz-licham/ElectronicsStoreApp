namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("mobile_specs")]
public class MobileSpecsRow
{
    [PrimaryKey] [References(typeof(ProductsRow))] public int ProductId { get; set; }
    public string ScreenSize { get; set; } = "";
    public string Storage { get; set; } = "";
    public string Battery { get; set; } = "";
    public string Connectivity { get; set; } = "";
    public string WaterResistance { get; set; } = "";
}
