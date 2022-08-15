using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomskiServer.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auction",
                columns: table => new
                {
                    id_auction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<int>(type: "int", nullable: false),
                    id_user = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.id_auction);
                    table.ForeignKey(
                        name: "FK_Auction_User_id_user",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auction_id_user",
                table: "Auction",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auction");
        }
    }
}
