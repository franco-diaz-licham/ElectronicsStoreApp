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
        db.CreateTableIfNotExists<SpecTypesRow>();
        db.CreateTableIfNotExists<ProductsRow>();
        db.CreateTableIfNotExists<GamingSpecsRow>();
        db.CreateTableIfNotExists<MobileSpecsRow>();
        db.CreateTableIfNotExists<TvSpecsRow>();
        db.CreateTableIfNotExists<PcSpecsRow>();

        // Example seed
        SeedData.SeedDbData(db);
    }
}
