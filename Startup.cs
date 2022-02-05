using digify.Modules;
using digify.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddDbContext<IContext, DatabaseContext>(ctx =>
            ctx.UseNpgsql(Configuration.GetValue<string>("database:postgresConnectionString"))
        );
        services.AddSingleton<IPasswordHasher, Argon2IdHasher>();
            services.AddSingleton<IAuthorization>((services) => jwtSigningKey == null
                ? new JWTAuthorization()
                : new JWTAuthorization(jwtSigningKey))
            ;
        services.AddSingleton<IFixtureLoader>((provider) => new FixtureLoader(
            provider.GetService<ILogger<FixtureLoader>>()!,
            provider.GetService<IPasswordHasher>()!
            ));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseCors(cors => cors
                .WithOrigins("https://localhost:44415")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        using (var scope = app.ApplicationServices.CreateScope())
        using (var db = scope.ServiceProvider.GetService<IContext>()!)
        {
            var fixtureLoader = scope.ServiceProvider.GetService<IFixtureLoader>()!;
            db.Database.Migrate();
            fixtureLoader.Load(db).Wait();
        }
    }
}