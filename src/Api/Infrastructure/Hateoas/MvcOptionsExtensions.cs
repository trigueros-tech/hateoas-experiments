using System.Text.Json;
using Api.Infrastructure.Hateoas;

namespace Microsoft.AspNetCore.Mvc;

public static class MvcOptionsExtensions
{
    public static MvcOptions AddHateoasFormatter(this MvcOptions options)
    {
        options.OutputFormatters.Insert(0, new JsonHalFormatter(new JsonSerializerOptions()));
        return options;
    }

}