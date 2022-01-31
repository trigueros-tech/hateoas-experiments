using Api.Model.Search;
using Api.Model.Search.Criteria;
using Api.Model.Search.Selection;
using Api.Model.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("searches")]
public class SearchController : ControllerBase
{
    [HttpPost]
    public Search PerformASearch([FromBody] Criteria criteria)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Search retrieveAnExistingSearch([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
    
    // TODO : V2 - Extract to their own controllers
    [HttpGet("{searchId}/spacetrains")]
    public List<SpaceTrain> RetrieveSpaceTrainsForBound([FromRoute]Guid searchId, [FromQuery]Bound bound, [FromQuery]bool onlySelectable = false)
    {
        throw new NotImplementedException();
    }

    [HttpPost("/{searchId}/spacetrains/{spaceTrainNumber}/fares/{fareId}/select")]
    public Selection SelectSpaceTrainWithFare([FromRoute] Guid searchId, [FromRoute] string spaceTrainNumber,
        [FromRoute] Guid fareId, [FromQuery] bool resetSelection = false)
    {
        throw new NotImplementedException();
    }

    [HttpGet("/{searchId}/selection")]
    public Selection SelectSpaceTrainWithFare([FromRoute] Guid searchId)
    {
        throw new NotImplementedException();
    }
}