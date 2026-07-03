using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoClub.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRolToEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Empleados",
                type: "text",
                nullable: false,
                defaultValue: "Empleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Empleados");
        }
    }
}
