using Apb.Domain.Facilities.Entities;
using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Apb.WebAPI.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace WebApiTests.Facilities;

public class GetFacility
{
    [Test]
    public async Task ReturnsOkIfExists()
    {
        const string facilityId = "00100001";
        var facilityView = new FacilityView(new Facility(facilityId));

        var mockLogger = new Mock<ILogger<FacilityController>>();
        var mockRepo = new Mock<IFacilityRepository>();
        mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<ApbFacilityId>()))
            .ReturnsAsync(facilityView);

        var controller = new FacilityController(mockRepo.Object, mockLogger.Object);
        var response = await controller.GetFacilityAsync(facilityId);

        Assert.Multiple(() =>
        {
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = response.Result as OkObjectResult;
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(facilityView);
        });
    }

    [Test]
    public async Task ReturnsNotFoundIfNotExists()
    {
        const string facilityId = "00100001";
        FacilityView? facilityView = null;

        var mockLogger = new Mock<ILogger<FacilityController>>();
        var mockRepo = new Mock<IFacilityRepository>();
        mockRepo.Setup(l => l.GetFacilityAsync(It.IsAny<ApbFacilityId>()))
            .ReturnsAsync(facilityView);

        var controller = new FacilityController(mockRepo.Object, mockLogger.Object);
        var response = await controller.GetFacilityAsync(facilityId);

        Assert.Multiple(() =>
        {
            response.Result.Should().BeOfType<NotFoundObjectResult>();
            var result = response.Result as NotFoundObjectResult;
            result!.StatusCode.Should().Be(404);
        });
    }

    [Test]
    public async Task ReturnsBadRequestIfInvalidFacilityId()
    {
        const string facilityId = "000";

        var mockRepo = new Mock<IFacilityRepository>();
        var mockLogger = new Mock<ILogger<FacilityController>>();

        var controller = new FacilityController(mockRepo.Object, mockLogger.Object);
        var response = await controller.GetFacilityAsync(facilityId);

        Assert.Multiple(() =>
        {
            response.Result.Should().BeOfType<BadRequestObjectResult>();
            var result = response.Result as BadRequestObjectResult;
            result!.StatusCode.Should().Be(400);
        });
    }
}
