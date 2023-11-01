using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ebay.Migrations
{
    /// <inheritdoc />
    public partial class fixModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_Cartid",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_Productid",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_Orderid",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_Productid",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_Orderid",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_Productid",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_Cartid",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_Productid",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Productid",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Cartid",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Productid",
                table: "CartItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Order_total",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Order_id",
                table: "OrderItems",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Product_id",
                table: "OrderItems",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_User_id",
                table: "Carts",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Cart_id",
                table: "CartItems",
                column: "Cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Product_id",
                table: "CartItems",
                column: "Product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_Cart_id",
                table: "CartItems",
                column: "Cart_id",
                principalTable: "Carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_Product_id",
                table: "CartItems",
                column: "Product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_User_id",
                table: "Carts",
                column: "User_id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_Order_id",
                table: "OrderItems",
                column: "Order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_Product_id",
                table: "OrderItems",
                column: "Product_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_Cart_id",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_Product_id",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_User_id",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_Order_id",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_Product_id",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_Order_id",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_Product_id",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Carts_User_id",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_Cart_id",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_Product_id",
                table: "CartItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Order_total",
                table: "Orders",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderItems",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Orderid",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Productid",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CartItems",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Cartid",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Productid",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Orderid",
                table: "OrderItems",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Productid",
                table: "OrderItems",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Cartid",
                table: "CartItems",
                column: "Cartid");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Productid",
                table: "CartItems",
                column: "Productid");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_Cartid",
                table: "CartItems",
                column: "Cartid",
                principalTable: "Carts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_Productid",
                table: "CartItems",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_Orderid",
                table: "OrderItems",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_Productid",
                table: "OrderItems",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id");
        }
    }
}
