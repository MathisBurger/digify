using digify.Modules;
using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<DatabaseContext>();
        services.AddSingleton<IPasswordHasher, Argon2idHasher>();
        services.AddSingleton<FixtureLoader>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FixtureLoader loader)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        loader.Load().Wait();
    }
}