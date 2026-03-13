# User Management Technical Test

## Structure
- `UserManagement.Api`          → .NET Web API (.NET 8/9)
- `UserManagement.Maui`         → .NET MAUI App
- `UserManagement.Api.Tests`    → NUnit backend tests
- `UserManagement.Maui.Tests`   → NUnit MAUI tests (at least one)

## How to run

1. Start the API
   cd src/UserManagement.Api
   dotnet run

   → runs on http://localhost:5000 (or https:5001)

2. Update MAUI HttpClient base address if needed (currently localhost:5000)

3. Run MAUI
   cd src/UserManagement.Maui
   dotnet build
   dotnet run --project UserManagement.Maui.csproj -f net9.0-android  (or -f net9.0-windows etc.)


   ## SOLID explanation (very brief)

- **S**ingle Responsibility → Controller thin, business logic in service
- **O**pen/Closed → Easy to replace in-memory store with real DB
- **L**iskov → No deep inheritance misused
- **I**nterface Segregation → Small focused interfaces
- **D**ependency Inversion → High-level modules depend on abstractions (IUserService)