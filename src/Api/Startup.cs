namespace Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
        app
            .UseRouting()
            .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}