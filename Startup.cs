using System.Text.Json.Serialization;
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

    /// <summary>
    /// Configures all services that are used
    /// with dependency injection
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        
        var jwtSigningKey = Configuration.GetValue<string>("Authorization:JWTSigningKey");
        services.AddControllers();
        services.AddDbContext<IContext, DatabaseContext>(ctx =>
            ctx.UseNpgsql(Configuration.GetValue<string>("DATABASE_STRING"))
                .EnableDetailedErrors()
        );
        services.AddSwaggerGen();
        services.AddSingleton<IPasswordHasher, Argon2IdHasher>();
            services.AddSingleton<IAuthorization>((services) => jwtSigningKey == null
                ? new JWTAuthorization()
                : new JWTAuthorization(jwtSigningKey))
            ;
        services.AddSingleton<IFixtureLoader>((provider) => new FixtureLoader(
            provider.GetService<ILogger<FixtureLoader>>()!,
            provider.GetService<IPasswordHasher>()!
            ));
        services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    }

    /// <summary>
    /// Configures the webapp 
    /// </summary>
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
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
        }
        

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseStaticFiles();

        using (var scope = app.ApplicationServices.CreateScope())
        using (var db = scope.ServiceProvider.GetService<IContext>()!)
        {
            db.Database.Migrate();
            var fixtureLoader = scope.ServiceProvider.GetService<IFixtureLoader>()!;
            fixtureLoader.Load(db).Wait();
        }
    }
}