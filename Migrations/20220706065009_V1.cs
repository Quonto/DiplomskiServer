using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductInformation",
                columns: table => new
                {
                    id_product_information = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInformation", x => x.id_product_information);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "UserInformation",
                columns: table => new
                {
                    id_user_information = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_user = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInformation", x => x.id_user_information);
                    table.ForeignKey(
                        name: "FK_UserInformation_User_id_user",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    id_group = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PictureId = table.Column<int>(type: "int", nullable: true),
                    id_category = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.id_group);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    price = table.Column<int>(type: "int", nullable: false),
                    number_of_wish = table.Column<int>(type: "int", nullable: false),
                    number_of_like = table.Column<int>(type: "int", nullable: false),
                    number_of_viewers = table.Column<int>(type: "int", nullable: false),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_group = table.Column<int>(type: "int", nullable: true),
                    id_user = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id_product);
                    table.ForeignKey(
                        name: "FK_Product_Group_id_group",
                        column: x => x.id_group,
                        principalTable: "Group",
                        principalColumn: "id_group",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_id_user",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    id_image = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_product = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.id_image);
                    table.ForeignKey(
                        name: "FK_Image_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    id_review = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mark = table.Column<int>(type: "int", nullable: false),
                    coment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_product = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.id_review);
                    table.ForeignKey(
                        name: "FK_Review_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id_category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id_category);
                    table.ForeignKey(
                        name: "FK_Category_Image_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Image",
                        principalColumn: "id_image",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_PictureId",
                table: "Category",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_id_category",
                table: "Group",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_Group_PictureId",
                table: "Group",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_id_product",
                table: "Image",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_Product_id_group",
                table: "Product",
                column: "id_group");

            migrationBuilder.CreateIndex(
                name: "IX_Product_id_user",
                table: "Product",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductInformation_ProductsId",
                table: "ProductProductInformation",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_id_product",
                table: "Review",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_id_user",
                table: "UserInformation",
                column: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Category_id_category",
                table: "Group",
                column: "id_category",
                principalTable: "Category",
                principalColumn: "id_category",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Image_PictureId",
                table: "Group",
                column: "PictureId",
                principalTable: "Image",
                principalColumn: "id_image",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Image_PictureId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Image_PictureId",
                table: "Group");

            migrationBuilder.DropTable(
                name: "ProductProductInformation");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "UserInformation");

            migrationBuilder.DropTable(
                name: "ProductInformation");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
