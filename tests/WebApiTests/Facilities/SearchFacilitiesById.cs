using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Apb.WebAPI.Facilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace WebApiTests.Facilities;

public class SearchFacilitiesById
{
    [Test]
    public async Task ReturnsOkIfValidRequest()
    {
        const string findString = "001";

        var mockLogger = new Mock<ILogger<FacilityController>>();
        var mockRepo = new Mock<IFacilityRepository>();
        mockRepo.Setup(l => l.SearchFacilitiesById(It.IsAny<string>()))
            .ReturnsAsync(new List<FacilityView>());

        var controller = new FacilityController(mockRepo.Object, mockLogger.Object);
        var response = await controller.SearchFacilitiesByIdAsync(findString);

        Assert.Multiple(() =>
        {
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = response.Result as OkObjectResult;
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<List<FacilityView>>();
        });
    }

    [Test]
    public async Task ReturnsBadRequestIfLengthLessThanMinimum()
    {
        const string facilityId = "0";

        var mockRepo = new Mock<IFacilityRepository>();
        var mockLogger = new Mock<ILogger<FacilityController>>();

        var controller = new FacilityController(mockRepo.Object, mockLogger.Object);
        var response = await controller.SearchFacilitiesByIdAsync(facilityId);

        Assert.Multiple(() =>
        {
            response.Result.Should().BeOfType<BadRequestObjectResult>();
            var result = response.Result as BadRequestObjectResult;
            result!.StatusCode.Should().Be(400);
        });
    }
}
