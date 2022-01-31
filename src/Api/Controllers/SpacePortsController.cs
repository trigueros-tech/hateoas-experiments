using Api.Model.SpacePort;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("spaceports")]
public class SpacePortsController : ControllerBase
{

    [HttpGet]
    public List<SpacePort> GetSpacePorts([FromQuery] string? withNameContaining)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public SpacePort GetSpacePortIdentifiedBy([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
    
}