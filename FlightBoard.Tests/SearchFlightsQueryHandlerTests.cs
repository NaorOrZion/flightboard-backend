using Xunit;
using Moq;
using FlightBoard.Application.Handlers;
using FlightBoard.Domain.Repositories;
using FlightBoard.Domain.Services;
using FlightBoard.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class SearchFlightsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsFlightsMatchingCriteria()
    {
        var repoMock = new Mock<IFlightRepository>();
        var statusServiceMock = new Mock<IFlightStatusService>();

        var allFlights = new List<Flight>
        {
            new Flight { Id = 1, FlightNumber = "IS100", Destination = "Paris", DepartureTime = DateTime.Now.AddMinutes(40), Gate = "A1" },
            new Flight { Id = 2, FlightNumber = "IS200", Destination = "London", DepartureTime = DateTime.Now.AddMinutes(20), Gate = "B2" }
        };

        repoMock.Setup(r => r.GetByStatusAndDestinationAsync(null, "Paris"))
            .ReturnsAsync(allFlights.Where(f => f.Destination.Contains("Paris")).ToList());

        statusServiceMock.Setup(s => s.CalculateFlightStatus(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(FlightStatusType.Scheduled);
        statusServiceMock.Setup(s => s.GetStatusDisplayName(It.IsAny<FlightStatusType>()))
            .Returns("Scheduled");

        var handler = new SearchFlightsQueryHandler(repoMock.Object, statusServiceMock.Object);

        var result = await handler.Handle(new SearchFlightsQuery(null, "Paris"), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Collection(result,
            item => Assert.Equal("IS100", item.FlightNumber)
        );
    }
} 