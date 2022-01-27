
using digify.Shared;

namespace digify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = Configuration.ParseConfig();
            var url = config.GetValue<string>("HostURL");

            return new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseUrls(config.GetValue<string>("HostURL") ?? "http://localhost:80")
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseStartup<Startup>();
        }
    }
}