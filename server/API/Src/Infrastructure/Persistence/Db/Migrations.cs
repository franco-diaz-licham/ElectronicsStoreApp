namespace API.Src.Infrastructure.Persistence.Db;

public static class Migrations
{
    public static void Run(Container c)
    {
        var dbFactory = c.Resolve<IDbConnectionFactory>();
        using var db = dbFactory.Open();
        db.CreateTableIfNotExists<BrandRow>();
        db.CreateTableIfNotExists<CategoryRow>();
        db.CreateTableIfNotExists<ProductRow>();
        // Example seed
        if (!db.Exists<BrandRow>(x => x.Name == "Acme")) db.Insert(new BrandRow { Id = 1, Name = "Acme" });
        if (!db.Exists<CategoryRow>(x => x.Name == "Laptops")) db.Insert(new CategoryRow { Id = 2, Name = "Laptops" });
    }
}
