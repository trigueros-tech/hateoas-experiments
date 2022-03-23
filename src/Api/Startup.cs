using Api.Infrastructure.Hateoas;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.AddHateoasFormatter();
        });

        services.AddScoped<LinksContext>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app
            .UseRouting()
            .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}