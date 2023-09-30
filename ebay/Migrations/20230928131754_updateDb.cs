using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ebay.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 2,
                column: "ProductId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "OrderDetailsId", "ProductId" },
                values: new object[] { 3, 5 });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "OrderDetailsId", "ProductId" },
                values: new object[] { 4, 6 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "OrderDetailsId", "ProductId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "OrderDetailsId", "ProductId" },
                values: new object[] { 3, 3 });
        }
    }
}
