using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupProductInformation");

            migrationBuilder.AddColumn<int>(
                name: "id_group",
                table: "ProductInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInformation_id_group",
                table: "ProductInformation",
                column: "id_group");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInformation_Group_id_group",
                table: "ProductInformation",
                column: "id_group",
                principalTable: "Group",
                principalColumn: "id_group",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInformation_Group_id_group",
                table: "ProductInformation");

            migrationBuilder.DropIndex(
                name: "IX_ProductInformation_id_group",
                table: "ProductInformation");

            migrationBuilder.DropColumn(
                name: "id_group",
                table: "ProductInformation");

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

            migrationBuilder.CreateIndex(
                name: "IX_GroupProductInformation_ProductInformationId",
                table: "GroupProductInformation",
                column: "ProductInformationId");
        }
    }
}
