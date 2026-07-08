using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoClub.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddElencoRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElencoRol",
                columns: table => new
                {
                    ElencoId = table.Column<long>(type: "bigint", nullable: false),
                    RolElencoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElencoRol", x => new { x.ElencoId, x.RolElencoId });
                    table.ForeignKey(
                        name: "FK_ElencoRol_Elenco_ElencoId",
                        column: x => x.ElencoId,
                        principalTable: "Elenco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElencoRol_RolesElenco_RolElencoId",
                        column: x => x.RolElencoId,
                        principalTable: "RolesElenco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElencoRol_RolElencoId",
                table: "ElencoRol",
                column: "RolElencoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElencoRol");
        }
    }
}
