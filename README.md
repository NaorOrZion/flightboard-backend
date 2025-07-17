# FlightBoard Backend

This repository contains the backend services for the FlightBoard project. It is built with ASP.NET Core and follows a clean architecture approach, separating concerns into API, Application, Domain, and Infrastructure layers.

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
   git clone https://github.com/yourusername/flightboard-backend.git
   cd flightboard-backend
   ```

2. **Restore dependencies:**

   ```sh
   dotnet restore
   ```

3. **Apply database migrations:**

   ```sh
   cd FlightBoard.API
   dotnet ef database update
   ```

   > Ensure your connection string is set in `appsettings.json`.

4. **Run the API:**
   ```sh
   dotnet run --project FlightBoard.API
   ```
   The API will start on `https://localhost:5001` or as configured.

## Testing

To run tests:

```sh
dotnet test
```

## API Endpoints

- The API exposes endpoints for managing flights (CRUD operations).
- For detailed API documentation, see the controllers in `FlightBoard.API/Controllers/`.

## Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License.
