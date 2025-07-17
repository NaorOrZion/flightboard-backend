using FlightBoard.Domain.Entities;

namespace FlightBoard.Domain.Services;

public interface IFlightStatusService
{
    FlightStatusType CalculateFlightStatus(DateTime departureTime, DateTime currentTime);
    string GetStatusDisplayName(FlightStatusType status);
} 