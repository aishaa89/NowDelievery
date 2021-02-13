using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NowDelivary.Data.Migrations
{
    public partial class addModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentityNumber",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyIncome",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Income = table.Column<double>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyIncome", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DelivarymanID = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    DelivarymanCost = table.Column<double>(nullable: false),
                    CustomerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlaceCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresse",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    SpecialMark = table.Column<string>(nullable: true),
                    AreaID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresse", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerAddresse_Area_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Area",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAddresse_AspNetUsers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaID = table.Column<int>(nullable: false),
                    PlaceCategoryID = table.Column<int>(nullable: false),
                    PlaceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Place_Area_AreaID",
                        column: x => x.AreaID,
                        principalTable: "Area",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Place_PlaceCategory_PlaceCategoryID",
                        column: x => x.PlaceCategoryID,
                        principalTable: "PlaceCategory",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MenuCategorie",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuCategoryType = table.Column<string>(nullable: true),
                    PlaceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategorie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MenuCategorie_Place_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Place",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OrderInformation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceID = table.Column<int>(nullable: false),
                    DelivaryCost = table.Column<double>(nullable: false),
                    OrderImage = table.Column<string>(nullable: true),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInformation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderInformation_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderInformation_Place_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Place",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItem = table.Column<string>(nullable: true),
                    ItemPrice = table.Column<double>(nullable: false),
                    MenuCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Menu_MenuCategorie_MenuCategoryID",
                        column: x => x.MenuCategoryID,
                        principalTable: "MenuCategorie",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OrderMenuItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderInformationID = table.Column<int>(nullable: false),
                    MenuID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMenuItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderMenuItems_Menu_MenuID",
                        column: x => x.MenuID,
                        principalTable: "Menu",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_OrderMenuItems_OrderInformation_OrderInformationID",
                        column: x => x.OrderInformationID,
                        principalTable: "OrderInformation",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresse_AreaID",
                table: "CustomerAddresse",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresse_CustomerID",
                table: "CustomerAddresse",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuCategoryID",
                table: "Menu",
                column: "MenuCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategorie_PlaceID",
                table: "MenuCategorie",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInformation_OrderID",
                table: "OrderInformation",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInformation_PlaceID",
                table: "OrderInformation",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMenuItems_MenuID",
                table: "OrderMenuItems",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMenuItems_OrderInformationID",
                table: "OrderMenuItems",
                column: "OrderInformationID");

            migrationBuilder.CreateIndex(
                name: "IX_Place_AreaID",
                table: "Place",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Place_PlaceCategoryID",
                table: "Place",
                column: "PlaceCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyIncome");

            migrationBuilder.DropTable(
                name: "CustomerAddresse");

            migrationBuilder.DropTable(
                name: "OrderMenuItems");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "OrderInformation");

            migrationBuilder.DropTable(
                name: "MenuCategorie");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "PlaceCategory");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "AspNetUsers");
        }
    }
}
