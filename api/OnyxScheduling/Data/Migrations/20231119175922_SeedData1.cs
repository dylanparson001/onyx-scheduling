using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnyxScheduling.Migrations.Data
{
    /// <inheritdoc />
    public partial class SeedData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Invoice_Items",
                columns: new[] { "Id", "Category_Id", "Item_Name", "Price" },
                values: new object[,]
                {
                    { 123, 0, "Test", 50.0 },
                    { 234, 0, "Item", 75.0 }
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Assigned_Customer_Id", "Assigned_Technician_Id" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Assigned_Customer_Id", "Assigned_Technician_Id" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "InvoiceInvoice_Item",
                columns: new[] { "InvoiceId", "InvoiceItemId" },
                values: new object[,]
                {
                    { 1, 123 },
                    { 2, 234 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 1, 123 });

            migrationBuilder.DeleteData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 2, 234 });

            migrationBuilder.DeleteData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.InsertData(
                table: "Invoice_Items",
                columns: new[] { "Id", "Category_Id", "Item_Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, "Roller", 50.0 },
                    { 2, 0, "Spring", 75.0 }
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Assigned_Customer_Id", "Assigned_Technician_Id" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Assigned_Customer_Id", "Assigned_Technician_Id" },
                values: new object[] { 0, 0 });

            migrationBuilder.InsertData(
                table: "InvoiceInvoice_Item",
                columns: new[] { "InvoiceId", "InvoiceItemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });
        }
    }
}
