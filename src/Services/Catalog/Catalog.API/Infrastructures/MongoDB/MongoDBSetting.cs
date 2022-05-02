using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
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

        await DB.InitAsync(databaseName, GetMongoClientSettings(connectionString));

        await DB.MigrateAsync();
    }

    public static IServiceCollection AddMongoDB(this IServiceCollection services)
    {
        var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        var connectionString = config.GetValue<string>(DatabaseSettings.ConnectionString);
        var databaseName = config.GetValue<string>(DatabaseSettings.DatabaseName);

        DB.InitAsync(databaseName, GetMongoClientSettings(connectionString)).Wait();

        DB.MigrateAsync().Wait();

        return services;
    }

    public static MongoClientSettings GetMongoClientSettings(string connectionString)
    {
        var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);

        mongoClientSettings.ClusterConfigurator = cb =>
        {
            cb.Subscribe<CommandStartedEvent>(e =>
            {
                Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}");
            });
        };

        return mongoClientSettings;
    }
}
