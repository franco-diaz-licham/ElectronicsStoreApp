namespace API.Src.Domain.Products;

public sealed class CategoryEntity: BaseEntity
{
    public string Name { get; private set; }
    private CategoryEntity(string name)
    {
        Name = name;
    }

    public static CategoryEntity Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        return new CategoryEntity(name.Trim());
    }
}

public enum CategoryEnum
{
    MobilePhones = 1,
    TVs = 2,
    Gaming = 3,
    Electronics = 4,
    PCs = 5,
    Laptops = 6
}
