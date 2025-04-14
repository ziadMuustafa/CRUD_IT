using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_1.Migrations
{
    /// <inheritdoc />
    public partial class Mig_RemoveTheRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catigories_CatigoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CatigoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CatigoryId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatigoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatigoryId",
                table: "Products",
                column: "CatigoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catigories_CatigoryId",
                table: "Products",
                column: "CatigoryId",
                principalTable: "Catigories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
