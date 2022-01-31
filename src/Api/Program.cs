using Api;
using Microsoft.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }

    private static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder<Startup>(args).Build();
    }
}