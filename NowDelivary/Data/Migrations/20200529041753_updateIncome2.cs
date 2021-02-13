using Microsoft.EntityFrameworkCore.Migrations;

namespace NowDelivary.Data.Migrations
{
    public partial class updateIncome2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Profit",
                table: "CompanyIncome",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profit",
                table: "CompanyIncome");
        }
    }
}
