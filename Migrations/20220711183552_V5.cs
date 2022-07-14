using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInformation_id_user",
                table: "UserInformation");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_id_user",
                table: "UserInformation",
                column: "id_user",
                unique: true,
                filter: "[id_user] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInformation_id_user",
                table: "UserInformation");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_id_user",
                table: "UserInformation",
                column: "id_user");
        }
    }
}
