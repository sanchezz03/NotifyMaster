using Flurl.Http;
using Microsoft.Extensions.Options;
using NotifyMaster.Common.ConfigurationModels;
using NotifyMaster.Common.ConfigurationOptions;

namespace NotifyMaster.Client.Extensions;

public static class FlurlClientExtension
{
    public static IServiceCollection AddFlurlClient(this IServiceCollection services)
    {
        services.ConfigureOptions();

        var configuration = services
           .BuildServiceProvider()
           .GetRequiredService(typeof(IOptions<ServerConnectionConfiguration>)) as IOptions<ServerConnectionConfiguration>;

        if (configuration == null || configuration.Value == null || configuration.Value.BaseAddress == null)
        {
            throw new Exception("Not found Hangfire configuration in config file");
        }

        var flurlHttp = new FlurlClient();
        flurlHttp.BaseUrl = configuration.Value.BaseAddress;

        return services
            .AddSingleton<IFlurlClient>(flurlHttp);
    }

    #region Private methods

    private static IServiceCollection ConfigureOptions(this IServiceCollection services)
    {
        return services.AddTransient<IConfigureOptions<ServerConnectionConfiguration>, ServerConnectionConfigurationOption>();
    }

    #endregion
}
