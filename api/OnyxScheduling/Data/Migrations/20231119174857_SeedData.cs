using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnyxScheduling.Migrations.Data
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Invoice_Items",
                columns: new[] { "Id", "Category_Id", "Item_Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, "Roller", 50.0 },
                    { 2, 0, "Spring", 75.0 }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Address", "Assigned_Customer_Id", "Assigned_Technician_Id", "CreatedDateTime", "CustomerId", "FinishedDateTime", "InvoiceId", "InvoiceNumber", "Processing_Status", "TechniciansId", "Total_Price" },
                values: new object[,]
                {
                    { 1, null, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "INV001", null, null, 0.0 },
                    { 2, null, 0, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "INV002", null, null, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "InvoiceInvoice_Item",
                columns: new[] { "InvoiceId", "InvoiceItemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Invoices");
        }
    }
}
