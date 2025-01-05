using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotifyMaster.Common.ConfigurationModels;

namespace NotifyMaster.Common.ConfigurationOptions;

public class ServerConnectionConfigurationOption : IConfigureOptions<ServerConnectionConfiguration>
{
    private readonly IConfigurationSection _configurationSection;

    public ServerConnectionConfigurationOption(IConfiguration configuration)
    {
        _configurationSection = configuration.GetSection(Constants.CONFIGURATION_SECTION_SERVER_CONNECTION);
    }

    public void Configure(ServerConnectionConfiguration options)
    {
        options.BaseAddress = _configurationSection?
            .GetValue<string>(nameof(ServerConnectionConfiguration.BaseAddress)) ??
            string.Empty;
    }
}
