using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "picture_url",
                table: "products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "product_brand_id",
                table: "products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "product_type_id",
                table: "products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "product_brands",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_types", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_product_brand_id",
                table: "products",
                column: "product_brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_product_type_id",
                table: "products",
                column: "product_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_product_brands_product_brand_id",
                table: "products",
                column: "product_brand_id",
                principalTable: "product_brands",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_product_types_product_type_id",
                table: "products",
                column: "product_type_id",
                principalTable: "product_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_product_brands_product_brand_id",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_product_types_product_type_id",
                table: "products");

            migrationBuilder.DropTable(
                name: "product_brands");

            migrationBuilder.DropTable(
                name: "product_types");

            migrationBuilder.DropIndex(
                name: "IX_products_product_brand_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_product_type_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "picture_url",
                table: "products");

            migrationBuilder.DropColumn(
                name: "price",
                table: "products");

            migrationBuilder.DropColumn(
                name: "product_brand_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "product_type_id",
                table: "products");
        }
    }
}
