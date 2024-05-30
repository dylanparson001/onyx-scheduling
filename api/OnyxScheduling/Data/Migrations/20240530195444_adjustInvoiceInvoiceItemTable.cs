using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class adjustInvoiceInvoiceItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 30, 15, 54, 44, 569, DateTimeKind.Local).AddTicks(2051), new DateTime(2024, 5, 30, 15, 54, 44, 569, DateTimeKind.Local).AddTicks(2093) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 30, 15, 54, 44, 569, DateTimeKind.Local).AddTicks(2098), new DateTime(2024, 5, 30, 15, 54, 44, 569, DateTimeKind.Local).AddTicks(2099) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
