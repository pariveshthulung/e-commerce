using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ebay.Migrations
{
    /// <inheritdoc />
    public partial class changePId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 1,
                column: "ProductId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 2,
                column: "ProductId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 3,
                column: "ProductId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 4,
                column: "ProductId",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 1,
                column: "ProductId",
                value: 2001);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 2,
                column: "ProductId",
                value: 2002);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 3,
                column: "ProductId",
                value: 2002);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 4,
                column: "ProductId",
                value: 2003);
        }
    }
}
