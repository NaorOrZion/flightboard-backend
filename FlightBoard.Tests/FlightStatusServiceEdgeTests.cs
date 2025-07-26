using Xunit;
using FlightBoard.Application.Services;
using FlightBoard.Domain.Entities;
using System;

public class FlightStatusServiceEdgeTests
{
    private readonly FlightStatusService _service = new();

    [Theory]
    [InlineData(30, FlightStatusType.Boarding)]
    [InlineData(0, FlightStatusType.Boarding)]
    [InlineData(-60, FlightStatusType.Departed)]
    [InlineData(-61, FlightStatusType.Landed)]
    public void CalculateFlightStatus_BoundaryCases(int minutesFromNow, FlightStatusType expected)
    {
        var now = DateTime.Now;
        var departure = now.AddMinutes(minutesFromNow);
        var result = _service.CalculateFlightStatus(departure, now);
        Assert.Equal(expected, result);
    }
} 