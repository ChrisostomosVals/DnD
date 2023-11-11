using DataAdapter.NoSql;
using DnD.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DnD.Api.Extensions
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services, Action<DataRepositoriesSettings> action)
        {
            var settings = new DataRepositoriesSettings();
            action.Invoke(settings);
            if (settings is null)
                throw new ArgumentNullException("settingsAction", "Repository Settings can't be null.");
            else if (string.IsNullOrWhiteSpace(settings.MongoConnectionString))
                throw new ArgumentNullException("settings.ConnectionString", "Repository Connection String can't be null or empty.");
            var connection = new MongoDbConnection(settings.MongoConnectionString);
            connection.UseDatabase(settings.MongoDatabase);
            services.AddSingleton<IMongoDbConnection>(connection);
            services.AddTransient<CharacterRepository>();
            services.AddTransient<ChapterRepository>();
            services.AddTransient<ClassCategoryRepository>();
            services.AddTransient<ClassRepository>();
            services.AddTransient<WorldObjectRepository>();
            services.AddTransient<LocationRepository>();
            services.AddTransient<RaceRepository>();
            services.AddTransient<RaceCategoryRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<UserRoleRepository>();

        }
        public class DataRepositoriesSettings
        {
            public string MongoConnectionString { get; set; }
            public string MongoDatabase { get; set; }
        }
    }
}
