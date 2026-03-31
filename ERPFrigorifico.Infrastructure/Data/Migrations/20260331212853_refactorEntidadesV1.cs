using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPFrigorifico.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class refactorEntidadesV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Ingresos",
                newName: "TipoIngreso");

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadoEn",
                table: "Proveedores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadoEn",
                table: "Operarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProveedorId",
                table: "Ingresos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CantidadAnimales",
                table: "Ingresos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSalida",
                table: "Ingresos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patente",
                table: "Ingresos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EliminadoEn",
                table: "Camiones",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EliminadoEn",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "EliminadoEn",
                table: "Operarios");

            migrationBuilder.DropColumn(
                name: "CantidadAnimales",
                table: "Ingresos");

            migrationBuilder.DropColumn(
                name: "FechaSalida",
                table: "Ingresos");

            migrationBuilder.DropColumn(
                name: "Patente",
                table: "Ingresos");

            migrationBuilder.DropColumn(
                name: "EliminadoEn",
                table: "Camiones");

            migrationBuilder.RenameColumn(
                name: "TipoIngreso",
                table: "Ingresos",
                newName: "Estado");

            migrationBuilder.AlterColumn<int>(
                name: "ProveedorId",
                table: "Ingresos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
