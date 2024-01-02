using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTareasApp.Migrations
{
    public partial class Agrego_FechaLimite_modelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLimite",
                table: "Tareas",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaLimite",
                table: "Tareas");
        }
    }
}
