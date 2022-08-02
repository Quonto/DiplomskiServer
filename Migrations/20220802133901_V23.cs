using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Group_GroupId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Product",
                newName: "id_group");

            migrationBuilder.RenameIndex(
                name: "IX_Product_GroupId",
                table: "Product",
                newName: "IX_Product_id_group");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Group_id_group",
                table: "Product",
                column: "id_group",
                principalTable: "Group",
                principalColumn: "id_group",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Group_id_group",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "id_group",
                table: "Product",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_id_group",
                table: "Product",
                newName: "IX_Product_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Group_GroupId",
                table: "Product",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "id_group",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
