namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("tv_specs")]
public class TvSpecsRow
{
    [PrimaryKey]
    [Alias("product_id"), References(typeof(ProductRow))] public int ProductId { get; set; }
    [Alias("screen_size")] public string ScreenSize { get; set; } = "";
    public string Resolution { get; set; } = "";
    [Alias("refresh_rate")] public string RefreshRate { get; set; } = "";
    public string Ports { get; set; } = "";
    [Alias("smart_os")] public string SmartOs { get; set; } = "";
}
