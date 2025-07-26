using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Services;

namespace FlightBoard.Application.Services;

public class FlightStatusService : IFlightStatusService
{
    public FlightStatusType CalculateFlightStatus(DateTime departureTime, DateTime currentTime)
    {
        var timeDifference = departureTime - currentTime;
        var totalMinutes = timeDifference.TotalMinutes;
        
        return totalMinutes switch
        {
            > 30 => FlightStatusType.Scheduled,
            <= 30 and >= 0 => FlightStatusType.Boarding,
            < 0 and >= -60 => FlightStatusType.Departed,
            < -60 => FlightStatusType.Landed,
            _ => FlightStatusType.Scheduled
        };
    }

    public string GetStatusDisplayName(FlightStatusType status)
    {
        return status switch
        {
            FlightStatusType.Scheduled => "Scheduled",
            FlightStatusType.Boarding => "Boarding",
            FlightStatusType.Departed => "Departed",
            FlightStatusType.Landed => "Landed",
            _ => "Unknown"
        };
    }
} 