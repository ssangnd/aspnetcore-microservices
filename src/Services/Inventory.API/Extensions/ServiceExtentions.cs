using Infrastructure.Extentions;
using MongoDB.Driver;

namespace Inventory.Product.API.Extensions
{
    public static class ServiceExtentions
    {
        internal static IServiceCollection AddConfigurationsSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>();
            services.AddSingleton(databaseSettings);

            return services;
        }

        private static string getMongoConnectionString(this IServiceCollection services)
        {
            var settings = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));
            if (services == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentNullException("Database is not configure.");
            }
            var databaseName = settings.DatabaseName;
            var mongoDbConnectionString = settings.ConnectionString + "/" + databaseName + "?authSource=admin";
            return mongoDbConnectionString;
        }

        public static void ConfigureMongoDbClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(new MongoClient(getMongoConnectionString(services)))
                .AddScoped(x=>x.GetService<IMongoClient>()?.StartSession());
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        }
    }
}
