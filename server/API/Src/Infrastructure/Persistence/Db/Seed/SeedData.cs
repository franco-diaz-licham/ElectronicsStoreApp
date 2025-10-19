namespace API.Src.Infrastructure.Persistence.Db.Seed;

public static class SeedData
{
    private const string BASE_PATH = "src/Infrastructure/Persistence/Db/Seed";
    private const string PRODUCTS = $"{BASE_PATH}/ProductsData.json";
    private const string BRANDS = $"{BASE_PATH}/BrandsData.json";
    private const string CATEGORIES = $"{BASE_PATH}/CategoriesData.json";
    private const string SPEC_TYPES = $"{BASE_PATH}/specTypesData.json";
    private const string MOBILE_SPECS = $"{BASE_PATH}/mobileSpecsData.json";
    private const string GAMING_SPECS = $"{BASE_PATH}/gamingSpecsData.json";
    private const string TV_SPECS = $"{BASE_PATH}/tvSpecsData.json";
    private const string PC_SPECS = $"{BASE_PATH}/pcSpecsData.json";

    /// <summary>
    /// Main Seed data controller.
    /// </summary>
    public static void SeedDbData(IDbConnection db)
    {
        db.SeedBrands();
        db.SeedCategories();
        db.SeedSpecTypes();
        db.SeedProducts();
        db.SeedMobileSpecs();
        db.SeedGamingSpecs();
        db.SeedTVSpecs();
        db.SeedPCSpecs();
    }

    private static void SeedBrands(this IDbConnection db)
    {
        if (db.SelectLazy<BrandsRow>().Any()) return;
        var data = File.ReadAllText(BRANDS);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<BrandsRow>>(data, opt);
        if (models is null) return;
        db.InsertAll(models);
    }

    private static void SeedCategories(this IDbConnection db)
    {
        if (db.SelectLazy<CategoriesRow>().Any()) return;
        var data = File.ReadAllText(CATEGORIES);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<CategoriesRow>>(data, opt);
        if (models is null) return;
        db.InsertAll(models);
    }

    private static void SeedSpecTypes(this IDbConnection db)
    {
        if (db.SelectLazy<SpecTypesRow>().Any()) return;
        var data = File.ReadAllText(SPEC_TYPES);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<SpecTypesRow>>(data, opt);
        if (models is null) return;
        db.InsertAll(models);
    }

    private static void SeedProducts(this IDbConnection db)
    {
        if (db.SelectLazy<ProductsRow>().Any()) return;
        var data = File.ReadAllText(PRODUCTS);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<ProductsRow>>(data, opt);
        if (models is null) return;
        db.InsertAll(models);
    }

    private static void SeedMobileSpecs(this IDbConnection db)
    {
        if (db.SelectLazy<MobileSpecsRow>().Any()) return;
        var data = File.ReadAllText(MOBILE_SPECS);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<MobileSpecsRow>>(data, opt);
        if (models is null) return;
        db.InsertAll(models);
    }

    private static void SeedGamingSpecs(this IDbConnection db)
    {
        if (db.SelectLazy<GamingSpecsRow>().Any()) return;
        var data = File.ReadAllText(GAMING_SPECS);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<GamingSpecsRow>>(data, opt);
        if (models is null) return;
        db.InsertAll(models);
    }

    private static void SeedTVSpecs(this IDbConnection db)
    {
        if (db.SelectLazy<TvSpecsRow>().Any()) return;
        var data = File.ReadAllText(TV_SPECS);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<TvSpecsRow>>(data, opt);
        if (models is null) return;
        db.InsertAll(models);
    }

    private static void SeedPCSpecs(this IDbConnection db)
    {
        if (db.SelectLazy<PcSpecsRow>().Any()) return;
        var data = File.ReadAllText(PC_SPECS);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var models = JsonSerializer.Deserialize<List<PcSpecsRow>>(data, opt);
        if (models is null) return;
        db.InsertAll(models);
    }
}
