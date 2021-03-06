﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NowDelivary.Data.Migrations
{
    public partial class addDelivaryCostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "DelivaryCost");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DelivaryCost",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    CustomerAreaID = table.Column<int>(type: "int", nullable: false),
                    ShoppingPlaceAreaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelivaryCost", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DelivaryCost_Area_CustomerAreaID",
                        column: x => x.CustomerAreaID,
                        principalTable: "Area",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DelivaryCost_Area_ShoppingPlaceAreaID",
                        column: x => x.ShoppingPlaceAreaID,
                        principalTable: "Area",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelivaryCost_CustomerAreaID",
                table: "DelivaryCost",
                column: "CustomerAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_DelivaryCost_ShoppingPlaceAreaID",
                table: "DelivaryCost",
                column: "ShoppingPlaceAreaID");
        }
    }
}
