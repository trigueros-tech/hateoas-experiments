using Api.Infrastructure.Hateoas;

namespace Microsoft.AspNetCore.Mvc;

public static class ControllerBaseExtensions
{
    public static void AddLink(this ControllerBase @this, Link link)
    {
        var context = @this.HttpContext.RequestServices.GetRequiredService<LinksContext>();
        context.Add(link);
    }
}