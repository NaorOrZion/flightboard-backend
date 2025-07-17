using Microsoft.AspNetCore.SignalR;

namespace FlightBoard.API.Hubs;

// 1. Inherit from Hub to create a SignalR hub
public class FlightBoardHub : Hub
{
    // 2. You can add methods here for client-server communication if needed
    // For now, we just use it for broadcasting
} 