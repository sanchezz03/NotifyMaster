namespace NotifyMaster.Common;

public static class Constants
{
    /// <summary>
    /// List Configuration Section Names in Configure file
    /// </summary>
    public const string CONFIGURATION_SECTION_TOKEN = "TelegramBot";
    public const string CONFIGURATION_SECTION_HANFIRE = "Hangfire";
    public const string CONFIGURATION_SECTION_DATABASE = "Database";
    public const string CONFIGURATION_SECTION_SERVER_CONNECTION = "ServerConnection";

    /// <summary>
    /// DATABASE
    /// </summary>
    public const string MIGRATION_PROJECT_NAME = "NotifyMaster.Infrastructure";
}