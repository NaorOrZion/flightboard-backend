using Xunit;
using Moq;
using FlightBoard.Application.Handlers;
using FlightBoard.Application.DTOs;
using FlightBoard.Domain.Repositories;
using System;
using System.Threading.Tasks;

public class CreateFlightCommandHandlerValidationTests
{
    [Fact]
    public async Task Handle_ThrowsException_WhenFlightNumberMissing()
    {
        var repoMock = new Mock<IFlightRepository>();
        repoMock.Setup(r => r.FlightNumberExistsAsync(It.IsAny<string>(), 0)).ReturnsAsync(false);
        var handler = new CreateFlightCommandHandler(repoMock.Object);
        var dto = new CreateFlightDto
        {
            FlightNumber = "",
            Destination = "Paris",
            DepartureTime = DateTime.UtcNow.AddHours(1),
            Gate = "A1"
        };
        await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(new CreateFlightCommand(dto), default));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenDestinationMissing()
    {
        var repoMock = new Mock<IFlightRepository>();
        repoMock.Setup(r => r.FlightNumberExistsAsync(It.IsAny<string>(), 0)).ReturnsAsync(false);
        var handler = new CreateFlightCommandHandler(repoMock.Object);
        var dto = new CreateFlightDto
        {
            FlightNumber = "IS700",
            Destination = "",
            DepartureTime = DateTime.UtcNow.AddHours(1),
            Gate = "A1"
        };
        await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(new CreateFlightCommand(dto), default));
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenGateMissing()
    {
        var repoMock = new Mock<IFlightRepository>();
        repoMock.Setup(r => r.FlightNumberExistsAsync(It.IsAny<string>(), 0)).ReturnsAsync(false);
        var handler = new CreateFlightCommandHandler(repoMock.Object);
        var dto = new CreateFlightDto
        {
            FlightNumber = "IS800",
            Destination = "Paris",
            DepartureTime = DateTime.UtcNow.AddHours(1),
            Gate = ""
        };
        await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(new CreateFlightCommand(dto), default));
    }
} 