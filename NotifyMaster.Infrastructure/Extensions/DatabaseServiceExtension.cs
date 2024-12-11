using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotifyMaster.Common;
using NotifyMaster.Common.ConfigurationModels;
using NotifyMaster.Common.ConfigurationOptions;
using NotifyMaster.Common.Helpers;
using NotifyMaster.Core.Interfaces;
using NotifyMaster.Infrastructure.Data;
using NotifyMaster.Infrastructure.Repositories;

namespace NotifyMaster.Infrastructure.Extensions;

public static class DatabaseServiceExtension
{
    #region Public methods

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.ConfigureOptions();

        var configuration = services
            .BuildServiceProvider()
            .GetRequiredService(typeof(IOptions<DatabaseConfiguration>)) as IOptions<DatabaseConfiguration>;

        if (configuration == null || configuration.Value == null || configuration.Value.ConnectionString == null)
        {
            throw new Exception("Not found DB configuration in config file");
        }

        services.AddDbContextPool<NotifyMasterDbContext>((services, options) =>
        {
            options.UseSqlServer(configuration.Value.ConnectionString, b => 
                b.MigrationsAssembly(Constants.MIGRATION_PROJECT_NAME));
        });

        SetDIRepository();

        return services;

        void SetDIRepository()
        {
            services.AddScoped(typeof(IRelationalRepository<>), typeof(BaseRelationalRepository<>));

            var relationalRepositiries = TypesHelper.GetDerivedTypesFromAssembly(typeof(BaseRelationalRepository<>));
            relationalRepositiries.ToList().ForEach(type => services.AddScoped(type));
        }
    }

    #endregion

    #region Private methods

    private static IServiceCollection ConfigureOptions(this IServiceCollection services)
    {
        return services.AddTransient<IConfigureOptions<DatabaseConfiguration>, DatabaseConfigurationOption>();
    }

    #endregion
}
