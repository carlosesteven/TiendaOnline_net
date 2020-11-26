using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaOnline.Data.Migrations
{
    public partial class cart_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductName",
                table: "Cart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductPicture",
                table: "Cart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductTotal",
                table: "Cart",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "ProductPicture",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "ProductTotal",
                table: "Cart");
        }
    }
}
