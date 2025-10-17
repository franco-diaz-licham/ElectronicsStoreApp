namespace API.Src.Infrastructure.Persistence.Db;

public static class OrmLiteBootstrap
{
    public static IDbConnectionFactory Create(string cs) => new OrmLiteConnectionFactory(cs, MySqlDialect.Provider);
}
