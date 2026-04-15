using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPFrigorifico.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class refactorEntidadesV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoFaena",
                table: "Faenas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoFaena",
                table: "Faenas");
        }
    }
}
