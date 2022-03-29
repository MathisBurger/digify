namespace digify.Shared;

/// <summary>
/// Basic configuration class
/// </summary>
public class Configuration
{
    /// <summary>
    /// Parses the config to a software readable format
    /// </summary>
    /// <param name="productionConfig">The filename of the production config</param>
    /// <returns>The configuration parsed into the config interface</returns>
    public static IConfiguration ParseConfig(string productionConfig = "appsettings.json") =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(productionConfig, optional: true)
            .AddJsonFile("appsettings.Development.json")
            .AddEnvironmentVariables(prefix: "DIGIFY_")
            .Build();
}