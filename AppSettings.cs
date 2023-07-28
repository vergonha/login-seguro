namespace secure_api;

public sealed class AppSettings
{
    private static IConfiguration configuration;

    static AppSettings()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appSettings.Development.json", optional: true, reloadOnChange: true);
        configuration = builder.Build();
    }

    public static string Get(string name)
    {
        string appSettings = configuration[name];
        return appSettings;
    }

    public static IConfigurationSection GetSection(string name)
    {
        return configuration.GetSection(name);
    }
}
