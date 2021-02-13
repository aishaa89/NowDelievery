using Microsoft.EntityFrameworkCore.Migrations;

namespace NowDelivary.Data.Migrations
{
    public partial class modifyOrderMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuItemQuantity",
                table: "OrderMenuItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuItemQuantity",
                table: "OrderMenuItems");
        }
    }
}
