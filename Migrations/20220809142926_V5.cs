using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_product_message",
                table: "ProductMessage",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductMessage",
                newName: "id_product_message");
        }
    }
}
