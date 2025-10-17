namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("product_categories")]
public class CategoryRow
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Index(Unique = true), StringLength(128)] public string Name { get; set; } = "";
}
