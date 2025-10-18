namespace API.Src.Infrastructure.Persistence.Db;

/// <summary>
/// Configurator class.
/// </summary>
public static class OrmLiteBootstrap
{
    public static IDbConnectionFactory Create(string connectionString)
    {
        OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
        OrmLiteConfig.DialectProvider.NamingStrategy = new LowercaseUnderscoreNamingStrategy();
        return new OrmLiteConnectionFactory(connectionString, OrmLiteConfig.DialectProvider);
    }
}
