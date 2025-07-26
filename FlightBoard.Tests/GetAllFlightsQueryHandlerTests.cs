using Xunit;
using Moq;
using FlightBoard.Application.Handlers;
using FlightBoard.Domain.Repositories;
using FlightBoard.Domain.Services;
using FlightBoard.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetAllFlightsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllFlightsWithStatus()
    {
        var repoMock = new Mock<IFlightRepository>();
        var statusServiceMock = new Mock<IFlightStatusService>();

        var flights = new List<Flight>
        {
            new Flight { Id = 1, FlightNumber = "IS100", Destination = "Paris", DepartureTime = DateTime.Now.AddMinutes(40), Gate = "A1" },
            new Flight { Id = 2, FlightNumber = "IS200", Destination = "London", DepartureTime = DateTime.Now.AddMinutes(20), Gate = "B2" }
        };

        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(flights);
        statusServiceMock.Setup(s => s.CalculateFlightStatus(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(FlightStatusType.Scheduled);
        statusServiceMock.Setup(s => s.GetStatusDisplayName(It.IsAny<FlightStatusType>()))
            .Returns("Scheduled");

        var handler = new GetAllFlightsQueryHandler(repoMock.Object, statusServiceMock.Object);

        var result = await handler.Handle(new GetAllFlightsQuery(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Collection(result,
            item => Assert.Equal("IS100", item.FlightNumber),
            item => Assert.Equal("IS200", item.FlightNumber)
        );
    }
} 