namespace API.Src.Domain.Products;

public sealed class SpecsTypeEntity : BaseEntity
{
    public string Name { get; private set; }

    private SpecsTypeEntity(string name)
    {
        Name = name;
    }

    public static SpecsTypeEntity Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        return new SpecsTypeEntity(name.Trim());
    }
}

public enum SpecTypeEnum
{
    Mobile = 1,
    TV = 2,
    Gaming = 3,
    PC = 4
}