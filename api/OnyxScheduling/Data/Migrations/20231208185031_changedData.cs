using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InvoiceInvoice_Item",
                columns: new[] { "InvoiceId", "InvoiceItemId" },
                values: new object[] { 2, 123 });

            migrationBuilder.UpdateData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "Item_Name", "Price" },
                values: new object[] { "234 Spring Red", 210.0 });

            migrationBuilder.UpdateData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 234,
                columns: new[] { "Item_Name", "Price" },
                values: new object[] { "4' Nylon Rollers", 15.0 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2023, 12, 8, 13, 50, 30, 985, DateTimeKind.Local).AddTicks(1347), new DateTime(2023, 12, 8, 13, 50, 30, 985, DateTimeKind.Local).AddTicks(1386) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2023, 12, 8, 13, 50, 30, 985, DateTimeKind.Local).AddTicks(1389), new DateTime(2023, 12, 8, 13, 50, 30, 985, DateTimeKind.Local).AddTicks(1390) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 2, 123 });

            migrationBuilder.UpdateData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "Item_Name", "Price" },
                values: new object[] { "Test", 50.0 });

            migrationBuilder.UpdateData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 234,
                columns: new[] { "Item_Name", "Price" },
                values: new object[] { "Item", 75.0 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2023, 12, 8, 11, 31, 20, 84, DateTimeKind.Local).AddTicks(3065), new DateTime(2023, 12, 8, 11, 31, 20, 84, DateTimeKind.Local).AddTicks(3109) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2023, 12, 8, 11, 31, 20, 84, DateTimeKind.Local).AddTicks(3112), new DateTime(2023, 12, 8, 11, 31, 20, 84, DateTimeKind.Local).AddTicks(3113) });
        }
    }
}
