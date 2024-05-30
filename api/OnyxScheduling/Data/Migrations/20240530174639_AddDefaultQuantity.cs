using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 1, 123 });

            migrationBuilder.DeleteData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 2, 123 });

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

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "Jobs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "DefaultQuantity",
                table: "Invoice_Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 30, 13, 46, 39, 315, DateTimeKind.Local).AddTicks(4115), new DateTime(2024, 5, 30, 13, 46, 39, 315, DateTimeKind.Local).AddTicks(4194) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 30, 13, 46, 39, 315, DateTimeKind.Local).AddTicks(4204), new DateTime(2024, 5, 30, 13, 46, 39, 315, DateTimeKind.Local).AddTicks(4206) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultQuantity",
                table: "Invoice_Items");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceNumber",
                table: "Jobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Invoice_Items",
                columns: new[] { "Id", "Category_Id", "Item_Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 123, 0, "234 Spring Red", 210.0, 0 },
                    { 234, 0, "4' Nylon Rollers", 15.0, 0 }
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 23, 14, 9, 35, 822, DateTimeKind.Local).AddTicks(2443), new DateTime(2024, 5, 23, 14, 9, 35, 822, DateTimeKind.Local).AddTicks(2487) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 23, 14, 9, 35, 822, DateTimeKind.Local).AddTicks(2492), new DateTime(2024, 5, 23, 14, 9, 35, 822, DateTimeKind.Local).AddTicks(2493) });

            migrationBuilder.InsertData(
                table: "InvoiceInvoice_Item",
                columns: new[] { "InvoiceId", "InvoiceItemId", "Quantity" },
                values: new object[,]
                {
                    { 1, 123, 2 },
                    { 2, 123, 10 },
                    { 2, 234, 0 }
                });
        }
    }
}
