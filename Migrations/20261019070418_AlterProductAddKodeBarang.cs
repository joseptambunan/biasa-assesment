using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetMvcLogin.Migrations
{
    /// <inheritdoc />
    public partial class AlterProductAddKodeBarang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "product_code",
                table: "tbl_product",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product_code",
                table: "tbl_product");
        }
    }
}
