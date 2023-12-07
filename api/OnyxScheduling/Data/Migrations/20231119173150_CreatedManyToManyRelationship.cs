using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedManyToManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Items_Invoices_Invoice_Id",
                table: "Invoice_Items");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_Items_Invoice_Id",
                table: "Invoice_Items");

            migrationBuilder.DropColumn(
                name: "Invoice_Id",
                table: "Invoice_Items");

            migrationBuilder.CreateTable(
                name: "InvoiceInvoice_Item",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceInvoice_Item", x => new { x.InvoiceId, x.InvoiceItemId });
                    table.ForeignKey(
                        name: "FK_InvoiceInvoice_Item_Invoice_Items_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "Invoice_Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceInvoice_Item_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceInvoice_Item_InvoiceItemId",
                table: "InvoiceInvoice_Item",
                column: "InvoiceItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceInvoice_Item");

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
    }
}
