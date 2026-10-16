using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetMvcLogin.Migrations
{
    /// <inheritdoc />
    public partial class AlterProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "tbl_product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "stock",
                table: "tbl_product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "tbl_product");

            migrationBuilder.DropColumn(
                name: "stock",
                table: "tbl_product");
        }
    }
}
