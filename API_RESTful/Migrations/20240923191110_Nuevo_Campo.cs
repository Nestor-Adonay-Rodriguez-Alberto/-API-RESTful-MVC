using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_RESTful.Migrations
{
    public partial class Nuevo_Campo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Fotografia",
                table: "Empleados",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fotografia",
                table: "Empleados");
        }
    }
}
