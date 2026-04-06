using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPFrigorifico.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class refactorEntidadesV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canal_Faenas_FaenaId",
                table: "Canal");

            migrationBuilder.DropForeignKey(
                name: "FK_Cortes_Canal_CanalId",
                table: "Cortes");

            migrationBuilder.DropForeignKey(
                name: "FK_Faenas_Animales_AnimalId",
                table: "Faenas");

            migrationBuilder.DropIndex(
                name: "IX_Faenas_AnimalId",
                table: "Faenas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Canal",
                table: "Canal");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Faenas");

            migrationBuilder.RenameTable(
                name: "Canal",
                newName: "Canales");

            migrationBuilder.RenameIndex(
                name: "IX_Canal_FaenaId",
                table: "Canales",
                newName: "IX_Canales_FaenaId");

            migrationBuilder.AddColumn<int>(
                name: "FaenaId",
                table: "Animales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Canales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Canales",
                table: "Canales",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Animales_FaenaId",
                table: "Animales",
                column: "FaenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Canales_AnimalId",
                table: "Canales",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animales_Faenas_FaenaId",
                table: "Animales",
                column: "FaenaId",
                principalTable: "Faenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Canales_Animales_AnimalId",
                table: "Canales",
                column: "AnimalId",
                principalTable: "Animales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Canales_Faenas_FaenaId",
                table: "Canales",
                column: "FaenaId",
                principalTable: "Faenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cortes_Canales_CanalId",
                table: "Cortes",
                column: "CanalId",
                principalTable: "Canales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animales_Faenas_FaenaId",
                table: "Animales");

            migrationBuilder.DropForeignKey(
                name: "FK_Canales_Animales_AnimalId",
                table: "Canales");

            migrationBuilder.DropForeignKey(
                name: "FK_Canales_Faenas_FaenaId",
                table: "Canales");

            migrationBuilder.DropForeignKey(
                name: "FK_Cortes_Canales_CanalId",
                table: "Cortes");

            migrationBuilder.DropIndex(
                name: "IX_Animales_FaenaId",
                table: "Animales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Canales",
                table: "Canales");

            migrationBuilder.DropIndex(
                name: "IX_Canales_AnimalId",
                table: "Canales");

            migrationBuilder.DropColumn(
                name: "FaenaId",
                table: "Animales");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Canales");

            migrationBuilder.RenameTable(
                name: "Canales",
                newName: "Canal");

            migrationBuilder.RenameIndex(
                name: "IX_Canales_FaenaId",
                table: "Canal",
                newName: "IX_Canal_FaenaId");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Faenas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Canal",
                table: "Canal",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Faenas_AnimalId",
                table: "Faenas",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Canal_Faenas_FaenaId",
                table: "Canal",
                column: "FaenaId",
                principalTable: "Faenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cortes_Canal_CanalId",
                table: "Cortes",
                column: "CanalId",
                principalTable: "Canal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faenas_Animales_AnimalId",
                table: "Faenas",
                column: "AnimalId",
                principalTable: "Animales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
