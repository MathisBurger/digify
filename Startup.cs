using digify.Modules;
using digify.Shared;

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
        
        var jwtSigningKey = Configuration.GetValue<string>("Authorization:JWTSigningKey");
        services.AddControllers();
        services.AddDbContext<DatabaseContext>();
        services.AddSingleton<IPasswordHasher, Argon2IdHasher>();
            services.AddSingleton<IAuthorization>((services) => jwtSigningKey == null
                ? new JWTAuthorization()
                : new JWTAuthorization(jwtSigningKey))
            ;
        services.AddSingleton<FixtureLoader>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FixtureLoader loader)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();
        }
        app.UseCors(options =>
        {
            options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

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