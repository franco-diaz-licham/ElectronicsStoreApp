namespace API.Src.Infrastructure.Persistence.Db.Rows;

[Alias("spec_types")]
public class SpecTypesRow
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Index(Unique = true), StringLength(128)] public string Name { get; set; } = "";
}
