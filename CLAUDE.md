# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands

```bash
# Build the entire solution
dotnet build BookSmartBackEnd.sln

# Run the API (launches on https://localhost:7200, http://localhost:5083)
dotnet run --project BookSmartBackEnd/BookSmartBackEnd.csproj

# Run unit tests (NUnit)
dotnet test BookSmartBackEndUnitTests/BookSmartBackEndUnitTests.csproj

# Run a single test by name
dotnet test BookSmartBackEndUnitTests/BookSmartBackEndUnitTests.csproj --filter "FullyQualifiedName~TestName"

# Add an EF Core migration (run from solution root)
dotnet ef migrations add MigrationName --project BookSmartBackEndDatabase --startup-project BookSmartBackEnd

# Apply migrations
dotnet ef database update --project BookSmartBackEndDatabase --startup-project BookSmartBackEnd
```

## Architecture

Three-project .NET 8.0 solution with a layered architecture:

- **BookSmartBackEnd** — ASP.NET Core Web API. Controllers → Business Logic (BLL) interfaces → implementations. DI is configured in `Program.cs`.
- **BookSmartBackEndDatabase** — Data access layer. Contains EF Core `BookSmartContext`, entity models, and migrations. Targets SQL Server.
- **BookSmartBackEndUnitTests** — NUnit 4 test project.

### Request Flow

```
Controller  →  IBll (interface)  →  Bll (implementation)  →  BookSmartContext (EF Core)  →  SQL Server
```

### Key Patterns

- **Interface-based DI**: `IUserBll`/`UserBll`, `IStaffBll`/`StaffBll`, `IAppointmentBll`/`AppointmentBll` registered as scoped services.
- **Controller routing**: `[Route("[controller]/[action]")]` — endpoints map to `/ControllerName/ActionName`.
- **DTOs**: Request models live in `Models/POST/`, response models in `Models/GET/`.
- **Query tracking disabled globally** via `UseQueryTrackingBehavior(NoTracking)`.

### Authentication

JWT Bearer tokens using HMAC SHA512. `JwtHelper` (registered as singleton) creates tokens with role-based claims. Authorization policies (e.g., `"Staff"`) are enforced via `[Authorize(Policy = "...")]`.

- JWT signing key: stored in user secrets as `jwtCert`
- DB password: stored in user secrets as `DbPassword`
- User secrets IDs are in each `.csproj` file

### Database Entities

Core tables: `User`, `Role` (junction), `RoleType`, `Business`, `Address`. Role types have predefined GUIDs defined in `Constants/RoleTypes.cs`. New users are auto-assigned the Client role on registration.