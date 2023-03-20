using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentEntity_Users_UserId",
                table: "RentEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentEntity",
                table: "RentEntity");

            migrationBuilder.RenameTable(
                name: "RentEntity",
                newName: "Rents");

            migrationBuilder.RenameIndex(
                name: "IX_RentEntity_UserId",
                table: "Rents",
                newName: "IX_Rents_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rents",
                table: "Rents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Users_UserId",
                table: "Rents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Users_UserId",
                table: "Rents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rents",
                table: "Rents");

            migrationBuilder.RenameTable(
                name: "Rents",
                newName: "RentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_UserId",
                table: "RentEntity",
                newName: "IX_RentEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentEntity",
                table: "RentEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentEntity_Users_UserId",
                table: "RentEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
