using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ebay.Migrations
{
    /// <inheritdoc />
    public partial class insertdataindb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "Brand", "Color", "Description", "Name", "Price", "Quantity", "Sold" },
                values: new object[,]
                {
                    { 1, null, "red", "This is nice phone.", "Iphone", 10000, 100, 0 },
                    { 2, null, "Green", "This is nice Samsung.", "SamSung", 50000, 100, 0 },
                    { 3, null, "Blue", "This is nice POCO.", "PoCO", 30000, 100, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
