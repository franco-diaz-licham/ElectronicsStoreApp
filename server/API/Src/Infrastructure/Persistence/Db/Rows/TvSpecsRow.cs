namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("tv_specs")]
public class TvSpecsRow
{
    [PrimaryKey]
    [References(typeof(ProductsRow))] public int ProductId { get; set; }
    public string ScreenSize { get; set; } = "";
    public string Resolution { get; set; } = "";
    public string RefreshRate { get; set; } = "";
    public string Ports { get; set; } = "";
    public string SmartOs { get; set; } = "";
}
