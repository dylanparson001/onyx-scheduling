using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInvoicesAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Items_Invoices_InvoicesId",
                table: "Invoice_Items");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_Items_InvoicesId",
                table: "Invoice_Items");

            migrationBuilder.DropColumn(
                name: "InvoicesId",
                table: "Invoice_Items");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Invoice_Id",
                table: "Invoice_Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Items_Invoice_Id",
                table: "Invoice_Items",
                column: "Invoice_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Items_Invoices_Invoice_Id",
                table: "Invoice_Items",
                column: "Invoice_Id",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Items_Invoices_Invoice_Id",
                table: "Invoice_Items");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_Items_Invoice_Id",
                table: "Invoice_Items");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Invoice_Id",
                table: "Invoice_Items");

            migrationBuilder.AddColumn<int>(
                name: "InvoicesId",
                table: "Invoice_Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Items_InvoicesId",
                table: "Invoice_Items",
                column: "InvoicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Items_Invoices_InvoicesId",
                table: "Invoice_Items",
                column: "InvoicesId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
