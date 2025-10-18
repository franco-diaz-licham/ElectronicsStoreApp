namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("pc_specs")]
public class PcSpecsRow
{
    [PrimaryKey][Alias("product_id"), References(typeof(ProductsRow))] public int ProductId { get; set; }
    public string Storage { get; set; } = "";
    public string Memory { get; set; } = "";
    public string Ports { get; set; } = "";
    public string Battery { get; set; } = "";
    public string Wireless { get; set; } = "";
    public string CPU { get; set; } = "";
}
