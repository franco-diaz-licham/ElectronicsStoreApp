namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("specs_type")]
public class SpecsTypeRow
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Index(Unique = true), StringLength(128)] public string Name { get; set; } = "";
}
