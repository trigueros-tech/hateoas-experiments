using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("")]
public class RootController : ControllerBase
{
    [HttpGet]
    public List<Link> Get()
    {
        var links = new List<Link>();
        var scheme = Request.Scheme;
        links.Add(new Link("searches", Url.Action("PerformASearch", "Search", new{}, scheme)));
        links.Add(new Link("bookings", Url.Action("RetrieveAnExistingBooking", "Bookings", new{}, scheme)));
        links.Add(new Link("self", Url.Action("Get", "Root", null, scheme)));
        links.Add(new Link("spaceports", Url.Action("GetSpacePorts", "SpacePorts", null, scheme)));
        return links;
    }

    public class Link
    {
        public string Name { get; init; }
        public string Href { get; init; }

        public Link(string name, string href)
        {
            Name = name;
            Href = href;
        }
    }
}