using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Group_GroupId",
                table: "Product",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "id_group",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Group_GroupId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Group",
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
    }
}
