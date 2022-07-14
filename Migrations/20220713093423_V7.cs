using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "number_of_wish",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "NumberOfWish",
                columns: table => new
                {
                    id_wish = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_product = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfWish", x => x.id_wish);
                    table.ForeignKey(
                        name: "FK_NumberOfWish_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumberOfWish_id_product",
                table: "NumberOfWish",
                column: "id_product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberOfWish");

            migrationBuilder.AddColumn<int>(
                name: "number_of_wish",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
