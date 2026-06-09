# VideoClubApp

## Structure

```
src/
├── VideoClub.Shared/       # Shared DTOs, Enums, Result<T>
│   ├── DTOs/
│   ├── Enums/
│   └── Result.cs
├── VideoClub.Api/          # Minimal API — entrypoint
│   ├── Features/
│   │   └── Productos/
│   │       ├── Commands/
│   │       ├── Handlers/
│   │       └── Validators/
│   ├── Controllers/
│   ├── Data/
│   │   ├── Entities/           # EF Core entity classes
│   │   ├── Configurations/     # EF Core IEntityTypeConfiguration
│   │   ├── AppDbContext.cs
│   │   └── ServiceCollectionExtensions.cs
│   ├── Middleware/
│   └── Program.cs
└── VideoClub.Client/       # Blazor WASM — entrypoint
    ├── Components/
    ├── Layout/
    ├── Pages/
    ├── Services/
    │   ├── Http/
    │   └── Interfaces/
    └── Program.cs
```

## Projects

| Project | TFM | Type | URL |
|---|---|---|---|
| `VideoClub.Api` | net10.0 | Minimal API | http://localhost:5144, OpenAPI UI at `/openapi-ui` |
| `VideoClub.Client` | net10.0 | Blazor WASM | WASM host |
| `VideoClub.Shared` | net10.0 | Class library | — |

- **Clean Architecture:** Client → Shared, Api → Shared. Client must not reference DB concerns. Shared stays dependency-free.

## Architecture conventions (intended)

- **CQRS:** Every Command/Query must have a corresponding Handler.
- **Validation:** `FluentValidation` in `PipelineBehaviors`.
- **Errors:** Return `ProblemDetails` (RFC 7807) or `Result<T>`.
- **Mapping:** `AutoMapper` for Shared DTOs → internal commands.
- **Security:** JWT middleware on the API.
- **DB:** PostgreSQL + Entity Framework Core via Npgsql. All DB code in `Data/`.
- **Client services:** Inject interfaces (`IProductService`), never `HttpClient` directly in components.
- **Client auth:** `DelegatingHandler` for automatic JWT injection.
- **UI:** Reusable components in `Components/`, page logic in `Pages/`. Use MudBlazor layout (`MudLayout`, `MudDrawer`, `MudAppBar`) over standard Blazor layout.
- **Client validation:** `MudForm` + `FluentValidation`, consistent with server.
- **Client icons:** `MudBlazor.Icons`.

## Commands

```powershell
# Start PostgreSQL
docker compose up -d

# Build all
dotnet build .\VideoClubApp.sln

# Run API
dotnet run --project .\src\VideoClub.Api

# Run client (requires API running)
dotnet run --project .\src\VideoClub.Client

# EF Core migrations
dotnet ef migrations add InitialCreate --project .\src\VideoClub.Api
dotnet ef database update --project .\src\VideoClub.Api
```

- No test projects exist yet.
- No CI/CD, no pre-commit hooks, no lint/formatter config beyond .NET defaults.

## Design System (Google Stitch — Systematic Professional)

### Tema MudBlazor
MudThemeProvider configurado con `MudTheme`:
- Primary:   `#402bca`
- Secondary: `#b70052`
- Error:     `#ba1a1a`
- Background:`#fcf8ff`
- Surface:   `#ffffff`
- Font:      Inter (importar en index.html)
- Border radius global: `8px` via `MudTheme.Shape`

### Reglas visuales globales
- Botones: sentence case via `MudGlobal.ButtonDefaults.ForceUppercase = false` en Program.cs
- Botón primario:   `Variant="Filled"   Color="Primary"`
- Botón secundario: `Variant="Outlined" Color="Secondary"`
- Labels de inputs siempre **encima** del campo (no flotantes): usar `<MudText>` separado + MudTextField sin `Label`
- Elevation: MudPaper `Elevation="1"`, Dialogs `Elevation="8"`

### Tablas / DataGrids
- Zebra striping con fila alterna `#FAFAFB` via CSS
- Headers sticky
- `Dense=true` en todas las vistas de datos
- Búsqueda posicionada arriba a la derecha

### Chips de estado
| Estado | Color | Variant |
|---|---|---|
| Activo / Devuelta | Success | Filled |
| Activa (renta) | Warning | Filled |
| Inactivo | Error | Filled |
- Todos con `border-radius: 999px` (pill shape) via CSS global

### Navegación
- MudDrawer `Variant="Permanent"`, ancho `240px`
- Item activo: fondo `rgba(64, 43, 202, 0.1)` + borde izquierdo `3px solid #402bca` via CSS

## Installed skills (`.agents/skills/`)

- `blazor-expert` — Blazor/MudBlazor conventions
- `dotnet-architect` — Clean Architecture guidance
- `dotnet-backend-patterns` — CQRS, MediatR, FluentValidation patterns
- `postgresql-table-design` — PostgreSQL schema design
