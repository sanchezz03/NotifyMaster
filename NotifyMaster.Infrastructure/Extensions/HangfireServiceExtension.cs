using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotifyMaster.Common.ConfigurationModels;
using NotifyMaster.Common.ConfigurationOptions;

namespace NotifyMaster.Infrastructure.Extensions;

public static class HangfireServiceExtension
{
    public static IServiceCollection AddHangfire(this IServiceCollection services)
    {
        services.ConfigureOptions();

        var configuration = services
            .BuildServiceProvider()
            .GetRequiredService(typeof(IOptions<HangfireConfiguration>)) as IOptions<HangfireConfiguration>;

        if (configuration == null || configuration.Value == null || configuration.Value.ConnectionString == null)
        {
            throw new Exception("Not found Hangfire configuration in config file");
        }

        return services.AddHangfire(cfg => cfg.UsePostgreSqlStorage(configuration.Value.ConnectionString))
            .AddHangfireServer();
    }

    #region Private methods

    private static IServiceCollection ConfigureOptions(this IServiceCollection services)
    {
        return services.AddTransient<IConfigureOptions<HangfireConfiguration>, HangfireConfigurationOption>();
    }

    #endregion
}
