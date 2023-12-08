using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "InvoiceInvoice_Item",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 1, 123 },
                column: "Quantity",
                value: 2);

            migrationBuilder.UpdateData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 2, 123 },
                column: "Quantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 2, 234 },
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2023, 12, 8, 14, 27, 11, 386, DateTimeKind.Local).AddTicks(1506), new DateTime(2023, 12, 8, 14, 27, 11, 386, DateTimeKind.Local).AddTicks(1551) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2023, 12, 8, 14, 27, 11, 386, DateTimeKind.Local).AddTicks(1553), new DateTime(2023, 12, 8, 14, 27, 11, 386, DateTimeKind.Local).AddTicks(1554) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InvoiceInvoice_Item");

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
    }
}
