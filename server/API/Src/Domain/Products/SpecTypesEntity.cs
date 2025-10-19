namespace API.Src.Domain.Products;

public sealed class SpecTypesEntity : BaseEntity
{
    public string Name { get; private set; }

    private SpecTypesEntity(string name)
    {
        Name = name;
    }

    public static SpecTypesEntity Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        return new SpecTypesEntity(name.Trim());
    }
}

public enum SpecTypeEnum
{
    Mobile = 1,
    TV = 2,
    Gaming = 3,
    PC = 4
}