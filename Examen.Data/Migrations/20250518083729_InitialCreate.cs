using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_persona",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    apellidoPaterno = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    apellidoMaterno = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    identificacion = table.Column<int>(type: "INTEGER", nullable: false),
                    token = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_persona", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_factura",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    id_persona = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_factura", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_factura_tbl_persona",
                        column: x => x.id_persona,
                        principalTable: "tbl_persona",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_factura_id_persona",
                table: "tbl_factura",
                column: "id_persona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_factura");

            migrationBuilder.DropTable(
                name: "tbl_persona");
        }
    }
}
