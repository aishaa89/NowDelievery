using Microsoft.EntityFrameworkCore.Migrations;

namespace NowDelivary.Data.Migrations
{
    public partial class updateIncome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DelivarymenCost",
                table: "CompanyIncome",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelivarymenCost",
                table: "CompanyIncome");
        }
    }
}
