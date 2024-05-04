public static class ConnectionHelper
{
    public static string GetConnectionString(IConfiguration configuration, IWebHostEnvironment environment
        )
    {
        //if (environmentName == "Development")
        if (environment.IsProduction())
        {


            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            return BuildConnectionString(databaseUrl!);
        }
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (connectionString == null)
        {

            throw new InvalidOperationException("Connection string not found");
        }
        return connectionString;

    }

    //build the connection string from the environment. i.e. Heroku
    private static string BuildConnectionString(string databaseUrl)
    {
        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/'),
            SslMode = Npgsql.SslMode.Require,
            TrustServerCertificate = true
        };
        return builder.ToString()!;
    }
}