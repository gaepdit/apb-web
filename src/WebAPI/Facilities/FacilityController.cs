using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Apb.WebAPI.Facilities;

[ApiController]
[Route("api/[controller]/{facilityId}")]
[Produces("application/json")]
public class FacilityController : ControllerBase
{
    private readonly ILogger<FacilityController> _logger;

    public FacilityController(ILogger<FacilityController> logger) => _logger = logger;

    [HttpGet]
    public async Task<ActionResult<FacilityView>> GetFacilityAsync(
        [FromServices] IFacilityRepository repository,
        [FromRoute] string facilityId)
    {
        _logger.LogInformation("Facility {@id} requested", new { id = facilityId });

        if (!ApbFacilityId.IsValidFacilityIdFormat(facilityId))
            return BadRequest("The requested Facility ID is not formatted correctly.");

        var item = await repository.GetFacilityAsync(facilityId);
        return item != null ? Ok(item) : NotFound("The requested Facility ID was not found.");
    }

    [HttpGet("exists")]
    public async Task<ActionResult<bool>> FacilityExistsAsync(
        [FromServices] IFacilityRepository repository,
        [FromRoute] string facilityId)
    {
        _logger.LogInformation("Facility {@id} exists requested", new { id = facilityId });

        if (!ApbFacilityId.IsValidFacilityIdFormat(facilityId))
            return BadRequest("The requested Facility ID is not formatted correctly.");

        return Ok(await repository.FacilityExistsAsync(facilityId));
    }
}
