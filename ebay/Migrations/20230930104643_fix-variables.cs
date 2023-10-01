using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ebay.Migrations
{
    /// <inheritdoc />
    public partial class fixvariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderDetails_OrderDetailsId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderDetailsId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "Orderid",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "id", "CustomerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 3 },
                    { 4, 2 }
                });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 1,
                column: "Orderid",
                value: null);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 2,
                column: "Orderid",
                value: null);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 3,
                column: "Orderid",
                value: null);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 4,
                column: "Orderid",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Orderid",
                table: "OrderItems",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Order_Orderid",
                table: "OrderItems",
                column: "Orderid",
                principalTable: "Order",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Order_Orderid",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_Orderid",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "OrderItems");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "id", "CustomerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 3 },
                    { 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderDetailsId",
                table: "OrderItems",
                column: "OrderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CustomerId",
                table: "OrderDetails",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderDetails_OrderDetailsId",
                table: "OrderItems",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
