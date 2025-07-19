using MediatR; // Register MediatR for request/response handling
using FlightBoard.Infrastructure; // Use our database/repository setup
using FlightBoard.Application.Services; // Use our business logic services
using FlightBoard.Application.Handlers; // For MediatR registration
using FlightBoard.Domain.Services; // Import the IFlightStatusService interface
using FlightBoard.API.Hubs; // Import the SignalR hub

var builder = WebApplication.CreateBuilder(args); // Create the web server builder

// Register MediatR and all handlers in the Application layer
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllFlightsQuery>());

// Register database and repositories
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=flightboard.db");

// Register business logic service
builder.Services.AddScoped<IFlightStatusService, FlightStatusService>();
Console.WriteLine("DB Connection String: " + (builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=flightboard.db"));
// Add controllers
builder.Services.AddControllers();

// Add CORS policy to allow requests from localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add SignalR services
builder.Services.AddSignalR();

var app = builder.Build(); // Build the web application

// Use the CORS policy before mapping controllers
app.UseCors("AllowLocal3000");

app.MapControllers(); // Use controllers for HTTP requests

// Map the SignalR hub endpoint
app.MapHub<FlightBoardHub>("/hubs/flightboard");

app.Run(); // Start the web server
