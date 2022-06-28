using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Apb.WebAPI.Facilities;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FacilityController : ControllerBase
{
    private readonly IFacilityRepository _repository;
    private readonly ILogger<FacilityController> _logger;

    public FacilityController(IFacilityRepository repository, ILogger<FacilityController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("{facilityId}")]
    public async Task<ActionResult<FacilityView>> GetFacilityAsync(
        [FromRoute] string facilityId)
    {
        _logger.LogInformation("Facility {@FacilityId} requested", new { facilityId });

        if (!ApbFacilityId.IsValidFacilityIdFormat(facilityId))
            return BadRequest("The requested Facility ID is not formatted correctly.");

        var item = await _repository.GetFacilityAsync(facilityId);
        return item is null ? NotFound("The requested Facility ID was not found.") : Ok(item);
    }

    [HttpGet("{facilityId}/exists")]
    public async Task<ActionResult<FacilityExistsResult>> FacilityExistsAsync(
        [FromRoute] string facilityId)
    {
        _logger.LogInformation("Facility {@FacilityId} exists requested", new { facilityId });

        if (!ApbFacilityId.IsValidFacilityIdFormat(facilityId))
            return BadRequest("The requested Facility ID is not formatted correctly.");

        return Ok(await _repository.FacilityExistsAsync(facilityId));
    }

    [HttpGet("find")]
    public async Task<ActionResult<List<FacilityView>>> SearchFacilitiesByIdAsync(
        [FromQuery] [MinLength(3)] string findFacilityId)
    {
        _logger.LogInformation("Find facility {@FindFacilityId} requested", new { findFacilityId });

        if (findFacilityId.Length < 3)
            return BadRequest("The requested facility ID fragment must be at least three characters.");

        return Ok(await _repository.SearchFacilitiesById(findFacilityId));
    }
}
