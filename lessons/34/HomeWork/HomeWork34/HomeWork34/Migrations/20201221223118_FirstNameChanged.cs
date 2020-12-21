using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeWork34.Migrations
{
    public partial class FirstNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocPositions_Name",
                table: "DocPositions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DocPositions");

            migrationBuilder.CreateIndex(
                name: "IX_DocPositions_FirstName",
                table: "DocPositions",
                column: "FirstName",
                unique: true,
                filter: "[FirstName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocPositions_FirstName",
                table: "DocPositions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DocPositions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DocPositions_Name",
                table: "DocPositions",
                column: "Name",
                unique: true);
        }
    }
}
