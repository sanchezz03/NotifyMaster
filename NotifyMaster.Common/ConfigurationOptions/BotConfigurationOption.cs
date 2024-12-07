using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NotifyMaster.Common.ConfigurationModels;

namespace NotifyMaster.Common.ConfigurationOptions;

public class BotConfigurationOption : IConfigureOptions<BotConfiguration>
{
    private readonly IConfigurationSection _configurationSection;

    public BotConfigurationOption(IConfiguration configuration)
    {
        _configurationSection = configuration.GetSection(Constants.CONFIGURATION_SECTION_TOKEN);
    }

    #region Public methods

    public void Configure(BotConfiguration options)
    {
        options.Token = _configurationSection?
            .GetValue<string>(nameof(BotConfiguration.Token)) ??
            string.Empty;
    }

    #endregion
}
