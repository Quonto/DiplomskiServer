using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UserID",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Product",
                newName: "id_user");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UserID",
                table: "Product",
                newName: "IX_Product_id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_id_user",
                table: "Product",
                column: "id_user",
                principalTable: "User",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_id_user",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "Product",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_id_user",
                table: "Product",
                newName: "IX_Product_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UserID",
                table: "Product",
                column: "UserID",
                principalTable: "User",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
