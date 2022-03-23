using Api.Infrastructure.Hateoas;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("")]
public class RootController : ControllerBase
{   
    [HttpGet]
    // Problem : don't works when void / Task / Ok()
    public object Get()
    {
        var scheme = Request.Scheme;
        this.AddLink(new Link("searches", Url.Action("PerformASearch", "Search", null, scheme), "GET"));
        this.AddLink(new Link("bookings", Url.Action("RetrieveAnExistingBooking", "Bookings", null, scheme), "GET"));
        this.AddLink(new Link("self", Url.Action("Get", "Root", null, scheme), "GET"));
        this.AddLink(new Link("spaceports", Url.Action("GetSpacePorts", "SpacePorts", null, scheme), "GET"));
        return new Object();
    }

}