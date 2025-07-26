using Xunit;
using Moq;
using FlightBoard.Application.Handlers;
using FlightBoard.Domain.Repositories;
using FlightBoard.Domain.Entities;

public class DeleteFlightCommandHandlerTests
{
    // should throw exception when flight not found
    [Fact]
    public async Task Handle_ThrowsException_WhenFlightNotFound()
    {
        var repoMock = new Mock<IFlightRepository>();
        repoMock.Setup(r => r.GetByIdAsync(123)).ReturnsAsync((Flight?)null);

        var handler = new DeleteFlightCommandHandler(repoMock.Object);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(new DeleteFlightCommand(123), default));
    }

    // should delete flight when found and return true
    [Fact]
    public async Task Handle_DeletesFlight_WhenFlightExists()
    {
        var repoMock = new Mock<IFlightRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Flight { Id = 1 });
        repoMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        var handler = new DeleteFlightCommandHandler(repoMock.Object);

        var result = await handler.Handle(new DeleteFlightCommand(1), default);

        Assert.True(result);
        repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }
} 