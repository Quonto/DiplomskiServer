using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProductInformation");

            migrationBuilder.DropColumn(
                name: "number_of_viewers",
                table: "Product");

            migrationBuilder.AddColumn<bool>(
                name: "is_admin",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GroupProductInformation",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    ProductInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupProductInformation", x => new { x.GroupsId, x.ProductInformationId });
                    table.ForeignKey(
                        name: "FK_GroupProductInformation_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "id_group",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupProductInformation_ProductInformation_ProductInformationId",
                        column: x => x.ProductInformationId,
                        principalTable: "ProductInformation",
                        principalColumn: "id_product_information",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumberOfViewe",
                columns: table => new
                {
                    id_viewe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_product = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfViewe", x => x.id_viewe);
                    table.ForeignKey(
                        name: "FK_NumberOfViewe_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupProductInformation_ProductInformationId",
                table: "GroupProductInformation",
                column: "ProductInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberOfViewe_id_product",
                table: "NumberOfViewe",
                column: "id_product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupProductInformation");

            migrationBuilder.DropTable(
                name: "NumberOfViewe");

            migrationBuilder.DropColumn(
                name: "is_admin",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "number_of_viewers",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductProductInformation",
                columns: table => new
                {
                    ProductInformationId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductInformation", x => new { x.ProductInformationId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductInformation_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductInformation_ProductInformation_ProductInformationId",
                        column: x => x.ProductInformationId,
                        principalTable: "ProductInformation",
                        principalColumn: "id_product_information",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductInformation_ProductsId",
                table: "ProductProductInformation",
                column: "ProductsId");
        }
    }
}
