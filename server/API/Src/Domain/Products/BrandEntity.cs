namespace API.Src.Domain.Products;

public sealed class BrandEntity : BaseEntity
{
    public string Name { get; private set; }
    private BrandEntity(string name)
    {
        Name = name;
    }

    public static BrandEntity Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        return new BrandEntity(name.Trim());
    }
}
