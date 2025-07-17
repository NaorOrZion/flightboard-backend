using Xunit;
using FlightBoard.Application.Services;
using FlightBoard.Domain.Entities;

public class FlightStatusServiceTests
{
    private readonly FlightStatusService _service = new();

    [Theory]
    [InlineData(40, FlightStatusType.Scheduled)]
    [InlineData(20, FlightStatusType.Boarding)]
    [InlineData(-10, FlightStatusType.Departed)]
    [InlineData(-70, FlightStatusType.Landed)]
    public void CalculateFlightStatus_ReturnsExpectedStatus(int minutesFromNow, FlightStatusType expected)
    {
        var now = DateTime.UtcNow;
        var departure = now.AddMinutes(minutesFromNow);

        var result = _service.CalculateFlightStatus(departure, now);

        Assert.Equal(expected, result);
    }
} 