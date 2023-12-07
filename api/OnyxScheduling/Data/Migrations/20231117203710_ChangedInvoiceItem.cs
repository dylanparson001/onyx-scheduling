using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedInvoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Items_Category_CategoryId",
                table: "Invoice_Items");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_Items_CategoryId",
                table: "Invoice_Items");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Invoice_Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Invoice_Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Items_CategoryId",
                table: "Invoice_Items",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Items_Category_CategoryId",
                table: "Invoice_Items",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
