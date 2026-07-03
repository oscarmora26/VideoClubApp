using Microsoft.EntityFrameworkCore;
using VideoClub.Api.Data.Entities;
using VideoClub.Shared.Enums;

namespace VideoClub.Api.Data;

public static class SeedData
{
    public static async Task SeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        if (await db.TiposArticulos.AnyAsync())
        {
            if (!await db.Clientes.AnyAsync())
            {
                db.Clientes.AddRange(
                    new Cliente { Nombre = "Carlos Martínez", Cedula = "001-1234567-8", NoTarjetaCr = "1234", LimiteCredito = 5000.00m, TipoPersona = "Física" },
                    new Cliente { Nombre = "Ana López", Cedula = "001-7654321-9", NoTarjetaCr = "5678", LimiteCredito = 8000.00m, TipoPersona = "Jurídica" });
                await db.SaveChangesAsync();
            }

            if (!await db.Rentas.AnyAsync())
            {
                var catchupTerminator = await db.Articulos.FirstOrDefaultAsync(a => a.Titulo == "Terminator");
                var catchupHangover = await db.Articulos.FirstOrDefaultAsync(a => a.Titulo == "The Hangover");
                var catchupJperez = await db.Empleados.FirstOrDefaultAsync(e => e.NombreUsuario == "jperez");
                var catchupMgarcia = await db.Empleados.FirstOrDefaultAsync(e => e.NombreUsuario == "mgarcia");
                var catchupCmartinez = await db.Clientes.FirstOrDefaultAsync(c => c.Cedula == "001-1234567-8");
                var catchupAlopez = await db.Clientes.FirstOrDefaultAsync(c => c.Cedula == "001-7654321-9");
                if (catchupTerminator is not null && catchupHangover is not null && catchupJperez is not null && catchupMgarcia is not null && catchupCmartinez is not null && catchupAlopez is not null)
                {
                    await SeedRentasAsync(db, catchupTerminator, catchupHangover, catchupJperez, catchupMgarcia, catchupCmartinez, catchupAlopez);
                }
            }
            return;
        }

        logger.LogInformation("Iniciando seed de la base de datos...");

        // Tipos de artículos
        var pelicula = new TipoArticulo { Descripcion = "Pelicula" };
        var cdMusica = new TipoArticulo { Descripcion = "CD Musica" };
        var libro = new TipoArticulo { Descripcion = "Libro" };
        db.TiposArticulos.AddRange(pelicula, cdMusica, libro);
        await db.SaveChangesAsync();

        logger.LogInformation("Tipos de artículos insertados.");

        // Géneros
        var accion = new Genero { Descripcion = "Acción" };
        var comedia = new Genero { Descripcion = "Comedia" };
        var drama = new Genero { Descripcion = "Drama" };
        var rock = new Genero { Descripcion = "Rock" };
        var pop = new Genero { Descripcion = "Pop" };
        var salsa = new Genero { Descripcion = "Salsa" };
        var novela = new Genero { Descripcion = "Novela" };
        var biografia = new Genero { Descripcion = "Biografía" };
        var fantasia = new Genero { Descripcion = "Fantasía" };
        db.Generos.AddRange(accion, comedia, drama, rock, pop, salsa, novela, biografia, fantasia);
        await db.SaveChangesAsync();

        logger.LogInformation("Géneros insertados.");

        // Idiomas
        var espanol = new Idioma { Descripcion = "Español" };
        var ingles = new Idioma { Descripcion = "Ingles" };
        db.Idiomas.AddRange(espanol, ingles);
        await db.SaveChangesAsync();

        logger.LogInformation("Idiomas insertados.");

        // Roles de elenco
        var actor = new RolElenco { Descripcion = "Actor" };
        var director = new RolElenco { Descripcion = "Director" };
        db.RolesElenco.AddRange(actor, director);
        await db.SaveChangesAsync();

        logger.LogInformation("Roles de elenco insertados.");

        // Elenco
        var arnold = new Elenco { Nombre = "Arnold Schwarzenegger" };
        var bradley = new Elenco { Nombre = "Bradley Cooper" };
        db.Elenco.AddRange(arnold, bradley);
        await db.SaveChangesAsync();

        logger.LogInformation("Elenco insertados.");

        // Relaciones TipoArticulo ↔ Genero
        db.TiposArticulosGeneros.AddRange(
            new TipoArticuloGenero { TipoArticulo = pelicula, Genero = accion },
            new TipoArticuloGenero { TipoArticulo = pelicula, Genero = comedia },
            new TipoArticuloGenero { TipoArticulo = pelicula, Genero = drama },
            new TipoArticuloGenero { TipoArticulo = cdMusica, Genero = rock },
            new TipoArticuloGenero { TipoArticulo = cdMusica, Genero = pop },
            new TipoArticuloGenero { TipoArticulo = cdMusica, Genero = salsa },
            new TipoArticuloGenero { TipoArticulo = libro, Genero = novela },
            new TipoArticuloGenero { TipoArticulo = libro, Genero = biografia },
            new TipoArticuloGenero { TipoArticulo = libro, Genero = fantasia });

        // Artículos
        var terminator = new Articulo { Titulo = "Terminator", TipoArticulo = pelicula, Genero = accion, Idioma = espanol, RentaPorDia = 2.5m, DiasRenta = 3, MontoEntregaTardia = 5, Stock = 10 };
        var hangover = new Articulo { Titulo = "The Hangover", TipoArticulo = pelicula, Genero = comedia, Idioma = ingles, RentaPorDia = 2.0m, DiasRenta = 3, MontoEntregaTardia = 4, Stock = 8 };
        db.Articulos.AddRange(
            terminator,
            hangover,
            new Articulo { Titulo = "Titanic", TipoArticulo = pelicula, Genero = drama, Idioma = espanol, RentaPorDia = 3.0m, DiasRenta = 5, MontoEntregaTardia = 6, Stock = 5 },
            new Articulo { Titulo = "Abbey Road", TipoArticulo = cdMusica, Genero = rock, Idioma = ingles, RentaPorDia = 1.5m, DiasRenta = 7, MontoEntregaTardia = 3, Stock = 15 },
            new Articulo { Titulo = "Thriller", TipoArticulo = cdMusica, Genero = pop, Idioma = ingles, RentaPorDia = 1.5m, DiasRenta = 7, MontoEntregaTardia = 3, Stock = 20 },
            new Articulo { Titulo = "A Puro Dolor", TipoArticulo = cdMusica, Genero = salsa, Idioma = espanol, RentaPorDia = 1.5m, DiasRenta = 7, MontoEntregaTardia = 3, Stock = 12 },
            new Articulo { Titulo = "Cien Años de Soledad", TipoArticulo = libro, Genero = novela, Idioma = espanol, RentaPorDia = 1.0m, DiasRenta = 14, MontoEntregaTardia = 2, Stock = 7 },
            new Articulo { Titulo = "Steve Jobs", TipoArticulo = libro, Genero = biografia, Idioma = ingles, RentaPorDia = 1.0m, DiasRenta = 14, MontoEntregaTardia = 2, Stock = 5 },
            new Articulo { Titulo = "El Nombre del Viento", TipoArticulo = libro, Genero = fantasia, Idioma = espanol, RentaPorDia = 1.0m, DiasRenta = 14, MontoEntregaTardia = 2, Stock = 6 },
            new Articulo { Titulo = "Inception", TipoArticulo = pelicula, Genero = accion, Idioma = ingles, RentaPorDia = 2.5m, DiasRenta = 3, MontoEntregaTardia = 5, Stock = 10 });

        await db.SaveChangesAsync();

        logger.LogInformation("Artículos insertados.");

        // Relaciones Elenco ↔ Artículo
        db.ElencosArticulos.AddRange(
            new ElencoArticulo { Articulo = terminator, Elenco = arnold, RolElenco = actor },
            new ElencoArticulo { Articulo = terminator, Elenco = arnold, RolElenco = director },
            new ElencoArticulo { Articulo = hangover, Elenco = bradley, RolElenco = actor },
            new ElencoArticulo { Articulo = hangover, Elenco = bradley, RolElenco = director });

        await db.SaveChangesAsync();

        logger.LogInformation("Relaciones elenco-artículo insertadas.");

        // Empleados
        var jperez = new Empleado { Nombre = "Juan Pérez", Cedula = "001-0000001-1", TandaLabor = TandaLabor.Matutina, PorcientoComision = 10m, FechaIngreso = new DateOnly(2024, 1, 15), NombreUsuario = "jperez", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), Rol = "Administrador" };
        var mgarcia = new Empleado { Nombre = "María García", Cedula = "001-0000002-2", TandaLabor = TandaLabor.Vespertina, PorcientoComision = 12m, FechaIngreso = new DateOnly(2024, 3, 1), NombreUsuario = "mgarcia", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), Rol = "Empleado" };
        db.Empleados.AddRange(jperez, mgarcia);

        await db.SaveChangesAsync();

        // Clientes
        var cmartinez = new Cliente { Nombre = "Carlos Martínez", Cedula = "001-1234567-8", NoTarjetaCr = "1234", LimiteCredito = 5000.00m, TipoPersona = "Física" };
        var alopez = new Cliente { Nombre = "Ana López", Cedula = "001-7654321-9", NoTarjetaCr = "5678", LimiteCredito = 8000.00m, TipoPersona = "Jurídica" };
        db.Clientes.AddRange(cmartinez, alopez);

        await db.SaveChangesAsync();

        // Rentas
        await SeedRentasAsync(db, terminator, hangover, jperez, mgarcia, cmartinez, alopez);

        logger.LogInformation("Seed completado exitosamente: 3 tipos, 9 géneros, 2 idiomas, 2 roles, 2 elencos, 10 artículos, 2 empleados, 2 clientes, 4 relaciones elenco-artículo, 2 rentas.");
    }

    private static DateTime UtcDate(int year, int month, int day) =>
        new(year, month, day, 0, 0, 0, DateTimeKind.Utc);

    private static async Task SeedRentasAsync(AppDbContext db, Articulo terminator, Articulo hangover, Empleado jperez, Empleado mgarcia, Cliente cmartinez, Cliente alopez)
    {
        var renta1 = new Renta
        {
            NoRenta = "R-001",
            Empleado = jperez,
            Cliente = cmartinez,
            FechaRenta = UtcDate(2025, 1, 10),
            FechaExpectedDevolucion = UtcDate(2025, 1, 13),
            FechaDevolucionReal = UtcDate(2025, 1, 13),
            MontoRetraso = 0,
            EstadoRenta = "Devuelta",
            Comentario = "Devuelta a tiempo",
            Detalles = new List<RentaDetalle>
            {
                new RentaDetalle { Articulo = terminator, MontoPorDia = 2.5m, CantidadDias = 3, FechaDevolucionEsperada = UtcDate(2025, 1, 13), FechaDevolucionReal = UtcDate(2025, 1, 13), Comentario = "Terminator" }
            }
        };

        var renta2 = new Renta
        {
            NoRenta = "R-002",
            Empleado = mgarcia,
            Cliente = alopez,
            FechaRenta = UtcDate(2025, 2, 15),
            FechaExpectedDevolucion = UtcDate(2025, 2, 18),
            MontoRetraso = 0,
            EstadoRenta = "Activa",
            Comentario = "Aún no devuelta",
            Detalles = new List<RentaDetalle>
            {
                new RentaDetalle { Articulo = hangover, MontoPorDia = 2.0m, CantidadDias = 3, FechaDevolucionEsperada = UtcDate(2025, 2, 18), Comentario = "The Hangover" }
            }
        };

        db.Rentas.AddRange(renta1, renta2);
        await db.SaveChangesAsync();
    }

}
