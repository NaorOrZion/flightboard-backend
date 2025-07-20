# FlightBoard Backend

This repository contains the backend services for the FlightBoard project. It is built with ASP.NET Core and follows a clean architecture approach, separating concerns into API, Application, Domain, and Infrastructure layers. The backend is designed to work with a separate React frontend (not included in this repository).

## Project Structure

- **FlightBoard.API/**: The main entry point for the backend API (ASP.NET Core Web API).
- **FlightBoard.Application/**: Contains business logic, application services, and DTOs.
- **FlightBoard.Domain/**: Contains core domain entities, interfaces, and domain logic.
- **FlightBoard.Infrastructure/**: Handles data access, database context, and external service integrations.
- **FlightBoard.Tests/**: Unit and integration tests for backend logic.
- **FlightBoard.sln**: Visual Studio solution file for the backend.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQLite (if using local db)
- (Optional) Visual Studio 2022+ or VS Code

## Setup Instructions

1. **Clone the repository:**

   ```sh
   git clone https://github.com/NaorOrZion/flightboard-backend.git
   cd flightboard-backend
   ```

2. **Restore dependencies:**

   ```sh
   dotnet restore
   ```

3. **Apply database migrations:**

   ```sh
   cd FlightBoard.API
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```

   > Ensure your connection string is set in `appsettings.json`.

4. **Run the API:**

   ```sh
   dotnet run --project FlightBoard.API
   ```

   The API will start on `https://localhost:5000` or as configured.

5. **Run backend tests:**
   ```sh
   dotnet test
   ```

## Architectural Choices

- **Clean Architecture:** The backend is organized into API, Application, Domain, and Infrastructure layers to enforce separation of concerns and testability.
- **CQRS with MediatR:** Command and Query Responsibility Segregation is implemented using MediatR for request/response handling.
- **Entity Framework Core:** Used for data access and migrations, with SQLite as the default local database.
- **SignalR:** Enables real-time updates between backend and frontend (e.g., for live flight board updates).
- **Frontend (Separate Project):** The backend is designed to work with a React frontend that uses Material UI for design, Redux Toolkit and React Query for state management and data fetching, and SignalR for real-time updates.

## Third-Party Libraries

### Backend

- **MediatR**: CQRS and mediator pattern for request/response handling
- **Microsoft.EntityFrameworkCore**: ORM for database access
- **Microsoft.EntityFrameworkCore.Sqlite**: SQLite provider for EF Core
- **Microsoft.AspNetCore.SignalR**: Real-time web functionality
- **Microsoft.AspNetCore.OpenApi**: OpenAPI/Swagger support

### Frontend (in separate project)

- **React**: UI library
- **Material UI, Emotion**: UI components and styling
- **Redux Toolkit, React Redux**: State management
- **React Query**: Data fetching and caching
- **axios**: HTTP client
- **@microsoft/signalr**: Real-time communication with backend
- **TypeScript**: Type safety
- **Testing Library, Jest**: Testing utilities

## API Endpoints

- The API exposes endpoints for managing flights (CRUD operations).
- For detailed API documentation, see the controllers in `FlightBoard.API/Controllers/`.

## Screen Recording (Optional)

You may include a short screen recording (e.g., GIF or video) of the running application here to demonstrate its features.

## Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License.
