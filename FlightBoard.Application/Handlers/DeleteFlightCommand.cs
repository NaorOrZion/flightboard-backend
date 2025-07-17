using MediatR;
using FlightBoard.Domain.Repositories;

namespace FlightBoard.Application.Handlers;

public record DeleteFlightCommand(int Id) : IRequest<bool>;

public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, bool>
{
    private readonly IFlightRepository _flightRepository;

    public DeleteFlightCommandHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<bool> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await _flightRepository.GetByIdAsync(request.Id);
        if (flight == null)
            throw new ArgumentException("Flight not found.");

        await _flightRepository.DeleteAsync(request.Id);
        return true;
    }
} 