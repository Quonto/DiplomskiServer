using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_user",
                table: "Review",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_id_user",
                table: "Review",
                column: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_User_id_user",
                table: "Review",
                column: "id_user",
                principalTable: "User",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_User_id_user",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_id_user",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "id_user",
                table: "Review");
        }
    }
}
