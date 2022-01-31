using System.Security.Cryptography.X509Certificates;
using Api.Model.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("bookings")]
public class BookingsController: ControllerBase
{
    [HttpPost]
    public Booking BookSomeSpaceTrainsFromTheSelectionOf([FromQuery] Guid searchId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Booking RetrieveAnExistingBooking([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}