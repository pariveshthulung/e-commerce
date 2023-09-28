using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ebay.Migrations
{
    /// <inheritdoc />
    public partial class changev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrderItems_OrderItemsid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderItemsid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderItemsid",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemsid",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1,
                column: "OrderItemsid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2,
                column: "OrderItemsid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 3,
                column: "OrderItemsid",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderItemsid",
                table: "Products",
                column: "OrderItemsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrderItems_OrderItemsid",
                table: "Products",
                column: "OrderItemsid",
                principalTable: "OrderItems",
                principalColumn: "id");
        }
    }
}
