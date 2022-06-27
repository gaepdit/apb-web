using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Apb.WebAPI.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace WebApiTests.Facilities;

public class FacilityExists
{
    [Test]
    public async Task ReturnsOkIfValidFacilityId()
    {
        const string facilityId = "00100001";
        var facilityExistsResult = new FacilityExistsResult(facilityId, true);

        var mockLogger = new Mock<ILogger<FacilityController>>().Object;
        var mockRepo = new Mock<IFacilityRepository>();
        mockRepo.Setup(l => l.FacilityExistsAsync(It.IsAny<ApbFacilityId>()))
            .ReturnsAsync(facilityExistsResult);

        var controller = new FacilityController(mockLogger);
        var response = await controller.FacilityExistsAsync(mockRepo.Object, facilityId);

        Assert.Multiple(() =>
        {
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = response.Result as OkObjectResult;
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(facilityExistsResult);
        });
    }

    [Test]
    public async Task ReturnsBadRequestIfInvalidFacilityId()
    {
        const string facilityId = "000";

        var mockRepo = new Mock<IFacilityRepository>();
        var mockLogger = new Mock<ILogger<FacilityController>>().Object;

        var controller = new FacilityController(mockLogger);
        var response = await controller.FacilityExistsAsync(mockRepo.Object, facilityId);

        Assert.Multiple(() =>
        {
            response.Result.Should().BeOfType<BadRequestObjectResult>();
            var result = response.Result as BadRequestObjectResult;
            result!.StatusCode.Should().Be(400);
        });
    }
}
