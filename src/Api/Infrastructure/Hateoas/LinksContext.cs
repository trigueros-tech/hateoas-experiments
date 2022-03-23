using System.Text.Json.Serialization;

namespace Api.Infrastructure.Hateoas;

public record LinksContext
{
    [JsonPropertyName("_links")]
    public List<Link> Links { get; init; } = new();

    public void Add(Link link)
    {
        Links.Add(link);
    }

    [JsonIgnore]
    public bool IsEmpty => Links.Count == 0;
}