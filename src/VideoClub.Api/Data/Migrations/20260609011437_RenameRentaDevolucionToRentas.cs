using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VideoClub.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameRentaDevolucionToRentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentaDevolucion");

            migrationBuilder.CreateTable(
                name: "Rentas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NoRenta = table.Column<string>(type: "text", nullable: false),
                    ClienteId = table.Column<long>(type: "bigint", nullable: false),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaRenta = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "now()"),
                    FechaExpectedDevolucion = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    FechaDevolucionReal = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    MontoTotal = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    MontoRetraso = table.Column<decimal>(type: "numeric(10,2)", nullable: false, defaultValue: 0.0m),
                    EstadoRenta = table.Column<string>(type: "text", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentas", x => x.Id);
                    table.CheckConstraint("CK_Rentas_EstadoRenta", "\"EstadoRenta\" IN ('Activa', 'Devuelta')");
                    table.CheckConstraint("CK_Rentas_FechaExpected", "\"FechaExpectedDevolucion\" > \"FechaRenta\"");
                    table.CheckConstraint("CK_Rentas_FechaReal", "\"FechaDevolucionReal\" IS NULL OR \"FechaDevolucionReal\" >= \"FechaRenta\"");
                    table.CheckConstraint("CK_Rentas_MontoRetraso", "\"MontoRetraso\" >= 0.0");
                    table.ForeignKey(
                        name: "FK_Rentas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentas_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentaDetalles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RentaId = table.Column<long>(type: "bigint", nullable: false),
                    ArticuloId = table.Column<long>(type: "bigint", nullable: false),
                    MontoPorDia = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CantidadDias = table.Column<int>(type: "integer", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "numeric(10,2)", nullable: false, computedColumnSql: "\"MontoPorDia\" * \"CantidadDias\"", stored: true),
                    FechaDevolucionEsperada = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    FechaDevolucionReal = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    DiasRetraso = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MontoRetraso = table.Column<decimal>(type: "numeric(10,2)", nullable: false, defaultValue: 0.0m),
                    Comentario = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentaDetalles", x => x.Id);
                    table.CheckConstraint("CK_RentaDetalles_CantidadDias", "\"CantidadDias\" > 0");
                    table.CheckConstraint("CK_RentaDetalles_DiasRetraso", "\"DiasRetraso\" >= 0");
                    table.CheckConstraint("CK_RentaDetalles_MontoPorDia", "\"MontoPorDia\" > 0");
                    table.CheckConstraint("CK_RentaDetalles_MontoRetraso", "\"MontoRetraso\" >= 0.0");
                    table.ForeignKey(
                        name: "FK_RentaDetalles_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentaDetalles_Rentas_RentaId",
                        column: x => x.RentaId,
                        principalTable: "Rentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentaDetalles_ArticuloId",
                table: "RentaDetalles",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_RentaDetalles_RentaId",
                table: "RentaDetalles",
                column: "RentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_ClienteId_FechaRenta",
                table: "Rentas",
                columns: new[] { "ClienteId", "FechaRenta" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_EmpleadoId",
                table: "Rentas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_EstadoRenta_FechaRenta",
                table: "Rentas",
                columns: new[] { "EstadoRenta", "FechaRenta" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_NoRenta",
                table: "Rentas",
                column: "NoRenta",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentaDetalles");

            migrationBuilder.DropTable(
                name: "Rentas");

            migrationBuilder.CreateTable(
                name: "RentaDevolucion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArticuloId = table.Column<long>(type: "bigint", nullable: false),
                    ClienteId = table.Column<long>(type: "bigint", nullable: false),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    cantidad_dias = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    DiasRetraso = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Estado = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "now()"),
                    fecha_devolucion = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    fecha_renta = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "now()"),
                    MontoXdia = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    NoRenta = table.Column<string>(type: "text", nullable: false),
                    usuario_creacion = table.Column<string>(type: "text", nullable: false),
                    usuario_modificacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentaDevolucion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentaDevolucion_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentaDevolucion_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentaDevolucion_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentaDevolucion_ArticuloId",
                table: "RentaDevolucion",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_RentaDevolucion_ClienteId",
                table: "RentaDevolucion",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RentaDevolucion_EmpleadoId",
                table: "RentaDevolucion",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RentaDevolucion_fecha_renta",
                table: "RentaDevolucion",
                column: "fecha_renta");

            migrationBuilder.CreateIndex(
                name: "IX_RentaDevolucion_NoRenta",
                table: "RentaDevolucion",
                column: "NoRenta",
                unique: true);
        }
    }
}
