using MongoDB.Driver;
using MongoDB.Entities;

namespace Catalog.API.Infrastructures.MongoDB;

internal class DatabaseSettings
{
    public const string ConnectionString = $"{nameof(DatabaseSettings)}:{nameof(ConnectionString)}";
    public const string DatabaseName = $"{nameof(DatabaseSettings)}:{nameof(DatabaseName)}";
}

public static class MongoDBSetting
{
    public static async Task UseMongoDB(this WebApplication app)
    {
        var connectionString = app.Configuration.GetValue<string>(DatabaseSettings.ConnectionString);
        var databaseName = app.Configuration.GetValue<string>(DatabaseSettings.DatabaseName);

        await DB.InitAsync(databaseName, MongoClientSettings.FromConnectionString(connectionString));

        await DB.MigrateAsync();
    }

    public static IServiceCollection AddMongoDB(this IServiceCollection services)
    {
        var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        var connectionString = config.GetValue<string>(DatabaseSettings.ConnectionString);
        var databaseName = config.GetValue<string>(DatabaseSettings.DatabaseName);

        DB.InitAsync(databaseName, MongoClientSettings.FromConnectionString(connectionString)).Wait();

        DB.MigrateAsync().Wait();

        return services;
    }
}
