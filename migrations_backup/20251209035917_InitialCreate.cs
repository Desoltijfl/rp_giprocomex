using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rp_giprocomex.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Puesto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Oficina = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Empresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RegistroPatronal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Siroc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NombreCompleto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaTermino = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Renovacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenovacionTermino = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Indeterminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
