namespace digify.Shared;

public class Configuration
{
    public static IConfiguration ParseConfig(string productionConfig = "appsettings.json") =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(productionConfig, optional: true)
            .AddJsonFile("appsettings.Development.json")
            .AddEnvironmentVariables(prefix: "DIGIFY_")
            .Build();
}