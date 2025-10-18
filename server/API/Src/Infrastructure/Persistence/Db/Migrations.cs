namespace API.Src.Infrastructure.Persistence.Db;

public static class Migrations
{
    public static void Run(Container c)
    {
        // init db connection
        var dbFactory = c.Resolve<IDbConnectionFactory>();
        using var db = dbFactory.Open();

        // migrate tables
        db.CreateTableIfNotExists<BrandsRow>();
        db.CreateTableIfNotExists<CategoriesRow>();
        db.CreateTableIfNotExists<SpecsTypeRow>();
        db.CreateTableIfNotExists<ProductsRow>();
        db.CreateTableIfNotExists<GamingSpecsRow>();
        db.CreateTableIfNotExists<MobileSpecsRow>();
        db.CreateTableIfNotExists<TvSpecsRow>();
        db.CreateTableIfNotExists<PcSpecsRow>();

        // Example seed
        if (!db.Exists<BrandsRow>(x => x.Name == "Acme")) db.Insert(new BrandsRow { Id = 1, Name = "Acme" });
        if (!db.Exists<CategoriesRow>(x => x.Name == "Laptops")) db.Insert(new CategoriesRow { Id = 2, Name = "Laptops" });
    }
}
