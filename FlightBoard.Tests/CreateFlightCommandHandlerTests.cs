using Xunit;
using Moq;
using FlightBoard.Application.Handlers;
using FlightBoard.Application.DTOs;
using FlightBoard.Domain.Repositories;
using FlightBoard.Domain.Entities;

public class CreateFlightCommandHandlerTests
{
    [Fact]
    public async Task Handle_ThrowsException_WhenFlightNumberExists()
    {
        var repoMock = new Mock<IFlightRepository>();
        repoMock.Setup(r => r.FlightNumberExistsAsync("DUPLICATE", 0)).ReturnsAsync(true);

        var handler = new CreateFlightCommandHandler(repoMock.Object);

        var dto = new CreateFlightDto
        {
            FlightNumber = "DUPLICATE",
            Destination = "Paris",
            DepartureTime = DateTime.Now.AddHours(1),
            Gate = "A1"
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(new CreateFlightCommand(dto), default));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenDepartureTimeInPast()
    {
        var repoMock = new Mock<IFlightRepository>();
        repoMock.Setup(r => r.FlightNumberExistsAsync(It.IsAny<string>(), 0)).ReturnsAsync(false);

        var handler = new CreateFlightCommandHandler(repoMock.Object);

        var dto = new CreateFlightDto
        {
            FlightNumber = "IS400",
            Destination = "Paris",
            DepartureTime = DateTime.Now.AddHours(-1),
            Gate = "A1"
        };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(new CreateFlightCommand(dto), default));
    }
} 