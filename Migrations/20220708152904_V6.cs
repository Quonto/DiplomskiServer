using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data",
                table: "ProductInformation");

            migrationBuilder.CreateTable(
                name: "ProductInformationData",
                columns: table => new
                {
                    id_product_information_data = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_product_information = table.Column<int>(type: "int", nullable: true),
                    id_product = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInformationData", x => x.id_product_information_data);
                    table.ForeignKey(
                        name: "FK_ProductInformationData_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInformationData_ProductInformation_id_product_information",
                        column: x => x.id_product_information,
                        principalTable: "ProductInformation",
                        principalColumn: "id_product_information",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInformationData_id_product",
                table: "ProductInformationData",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInformationData_id_product_information",
                table: "ProductInformationData",
                column: "id_product_information");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInformationData");

            migrationBuilder.AddColumn<string>(
                name: "data",
                table: "ProductInformation",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
