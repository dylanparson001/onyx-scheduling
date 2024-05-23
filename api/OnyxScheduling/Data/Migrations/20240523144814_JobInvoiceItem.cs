using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobInvoiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 23, 10, 48, 13, 969, DateTimeKind.Local).AddTicks(8665), new DateTime(2024, 5, 23, 10, 48, 13, 969, DateTimeKind.Local).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 23, 10, 48, 13, 969, DateTimeKind.Local).AddTicks(8715), new DateTime(2024, 5, 23, 10, 48, 13, 969, DateTimeKind.Local).AddTicks(8716) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 23, 10, 29, 51, 3, DateTimeKind.Local).AddTicks(5038), new DateTime(2024, 5, 23, 10, 29, 51, 3, DateTimeKind.Local).AddTicks(5082) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 23, 10, 29, 51, 3, DateTimeKind.Local).AddTicks(5087), new DateTime(2024, 5, 23, 10, 29, 51, 3, DateTimeKind.Local).AddTicks(5089) });
        }
    }
}
