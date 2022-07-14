using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "number_of_like",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "NumberOfLike",
                columns: table => new
                {
                    id_like = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_product = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfLike", x => x.id_like);
                    table.ForeignKey(
                        name: "FK_NumberOfLike_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumberOfLike_id_product",
                table: "NumberOfLike",
                column: "id_product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberOfLike");

            migrationBuilder.AddColumn<int>(
                name: "number_of_like",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
