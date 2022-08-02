using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    id_place = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.id_place);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_admin = table.Column<bool>(type: "bit", nullable: false),
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
                    nameUser = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    surename = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    buy = table.Column<bool>(type: "bit", nullable: false),
                    id_user_buy = table.Column<int>(type: "int", nullable: false),
                    add_to_cart = table.Column<bool>(type: "bit", nullable: false),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "ProductInformation",
                columns: table => new
                {
                    id_product_information = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    id_group = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInformation", x => x.id_product_information);
                    table.ForeignKey(
                        name: "FK_ProductInformation_Group_id_group",
                        column: x => x.id_group,
                        principalTable: "Group",
                        principalColumn: "id_group",
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

            migrationBuilder.CreateTable(
                name: "PlaceProductUser",
                columns: table => new
                {
                    id_places_product_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    id_user_information = table.Column<int>(type: "int", nullable: true),
                    id_product = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceProductUser", x => x.id_places_product_user);
                    table.ForeignKey(
                        name: "FK_PlaceProductUser_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaceProductUser_UserInformation_id_user_information",
                        column: x => x.id_user_information,
                        principalTable: "UserInformation",
                        principalColumn: "id_user_information",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    id_review = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mark = table.Column<int>(type: "int", nullable: false),
                    coment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_product = table.Column<int>(type: "int", nullable: true),
                    id_user = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Review_User_id_user",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInformationData",
                columns: table => new
                {
                    id_product_information_data = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_product_information_save = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "IX_NumberOfLike_id_product",
                table: "NumberOfLike",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_NumberOfViewe_id_product",
                table: "NumberOfViewe",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_NumberOfWish_id_product",
                table: "NumberOfWish",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceProductUser_id_product",
                table: "PlaceProductUser",
                column: "id_product",
                unique: true,
                filter: "[id_product] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceProductUser_id_user_information",
                table: "PlaceProductUser",
                column: "id_user_information",
                unique: true,
                filter: "[id_user_information] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_id_group",
                table: "Product",
                column: "id_group");

            migrationBuilder.CreateIndex(
                name: "IX_Product_id_user",
                table: "Product",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInformation_id_group",
                table: "ProductInformation",
                column: "id_group");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInformationData_id_product",
                table: "ProductInformationData",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInformationData_id_product_information",
                table: "ProductInformationData",
                column: "id_product_information");

            migrationBuilder.CreateIndex(
                name: "IX_Review_id_product",
                table: "Review",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_Review_id_user",
                table: "Review",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_id_user",
                table: "UserInformation",
                column: "id_user",
                unique: true,
                filter: "[id_user] IS NOT NULL");

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
                name: "NumberOfLike");

            migrationBuilder.DropTable(
                name: "NumberOfViewe");

            migrationBuilder.DropTable(
                name: "NumberOfWish");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "PlaceProductUser");

            migrationBuilder.DropTable(
                name: "ProductInformationData");

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
