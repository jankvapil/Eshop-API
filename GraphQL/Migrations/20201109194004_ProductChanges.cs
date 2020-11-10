using Microsoft.EntityFrameworkCore.Migrations;

namespace Eshop.GraphQL.Migrations
{
    public partial class ProductChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrderItems_OrderItemId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderItemId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Products",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderItemId",
                table: "Products",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrderItems_OrderItemId",
                table: "Products",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
