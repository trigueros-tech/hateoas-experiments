using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Api.Infrastructure.Hateoas;

public class JsonHalFormatter : TextOutputFormatter
{
    private JsonSerializerOptions SerializerOptions { get; init; }

    public JsonHalFormatter(JsonSerializerOptions jsonSerializerOptions)
    {
        SerializerOptions = jsonSerializerOptions;
        
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/json"));
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/*+json"));
    }
    
    // Slightly adapted from .NET6 Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        // From the original
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (selectedEncoding == null)
        {
            throw new ArgumentNullException(nameof(selectedEncoding));
        }

        var httpContext = context.HttpContext;

        // context.ObjectType reflects the declared model type when specified.
        // For polymorphic scenarios where the user declares a return type, but returns a derived type,
        // we want to serialize all the properties on the derived type. This keeps parity with
        // the behavior you get when the user does not declare the return type and with Json.Net at least at the top level.
        var objectType = context.Object?.GetType() ?? context.ObjectType ?? typeof(object);


        Stream objectStream = new MemoryStream();

        var linkContext = httpContext.RequestServices.GetRequiredService<LinksContext>();

        await JsonSerializer.SerializeAsync(objectStream, context.Object, objectType, SerializerOptions,
            httpContext.RequestAborted);

        
        var linksStream = new MemoryStream();
        if (!linkContext.IsEmpty)
        {
            await JsonSerializer.SerializeAsync(linksStream, linkContext, typeof(LinksContext), SerializerOptions);
            objectStream = Merge(linksStream, objectStream);
            objectStream.Seek(0, SeekOrigin.Begin);
        }
        
        var responseStream = httpContext.Response.Body;
        if (selectedEncoding.CodePage == Encoding.UTF8.CodePage)
        {
            try
            {
                await objectStream.CopyToAsync(responseStream);
                await responseStream.FlushAsync(httpContext.RequestAborted);
            }
            catch (OperationCanceledException)
            {
            }
        }
        else
        {
            // JsonSerializer only emits UTF8 encoded output, but we need to write the response in the encoding specified by
            // selectedEncoding
            var transcodingStream = Encoding.CreateTranscodingStream(httpContext.Response.Body, selectedEncoding,
                Encoding.UTF8, leaveOpen: true);

            ExceptionDispatchInfo? exceptionDispatchInfo = null;
            try
            {
                await objectStream.CopyToAsync(transcodingStream, httpContext.RequestAborted);
                await transcodingStream.FlushAsync();
            }
            catch (Exception ex)
            {
                // TranscodingStream may write to the inner stream as part of it's disposal.
                // We do not want this exception "ex" to be eclipsed by any exception encountered during the write. We will stash it and
                // explicitly rethrow it during the finally block.
                exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex);
            }
            finally
            {
                try
                {
                    await transcodingStream.DisposeAsync();
                }
                catch when (exceptionDispatchInfo != null)
                {
                }

                exceptionDispatchInfo?.Throw();
            }
        }
    }

    // From : https://github.com/dotnet/runtime/issues/31433#issuecomment-570475853
    // don't works with primitive types such as strings in ojbectContents.
    private Stream Merge(Stream linksStream, Stream objectStream)
    {
        linksStream.Seek(0, SeekOrigin.Begin);
        objectStream.Seek(0, SeekOrigin.Begin);
        var ms = new MemoryStream();
        using var jDoc1 = JsonDocument.Parse(linksStream);
        using var jDoc2 = JsonDocument.Parse(objectStream);
        using var jsonWriter = new Utf8JsonWriter(ms, new JsonWriterOptions());

        var root1 = jDoc1.RootElement;
        var root2 = jDoc2.RootElement;

        // Assuming both JSON strings are single JSON objects (i.e. {...})
        Debug.Assert(root1.ValueKind == JsonValueKind.Object);
        Debug.Assert(root2.ValueKind == JsonValueKind.Object);
        
        jsonWriter.WriteStartObject();
        
        // Write all the properties of the first document that don't conflict with the second
        foreach (var property in root1.EnumerateObject())
        {
            if (!root2.TryGetProperty(property.Name, out _))
            {
                property.WriteTo(jsonWriter);
            }
        }

        // Write all the properties of the second document (including those that are duplicates which were skipped earlier)
        // The property values of the second document completely override the values of the first
        foreach (var property in root2.EnumerateObject())
        {
            property.WriteTo(jsonWriter);
        }

        jsonWriter.WriteEndObject();
        return ms;
    }
}