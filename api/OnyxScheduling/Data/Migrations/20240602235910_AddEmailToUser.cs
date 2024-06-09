using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 6, 2, 19, 59, 10, 184, DateTimeKind.Local).AddTicks(7484), new DateTime(2024, 6, 2, 19, 59, 10, 184, DateTimeKind.Local).AddTicks(7558) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 6, 2, 19, 59, 10, 184, DateTimeKind.Local).AddTicks(7568), new DateTime(2024, 6, 2, 19, 59, 10, 184, DateTimeKind.Local).AddTicks(7571) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 31, 14, 18, 29, 102, DateTimeKind.Local).AddTicks(1834), new DateTime(2024, 5, 31, 14, 18, 29, 102, DateTimeKind.Local).AddTicks(1879) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 31, 14, 18, 29, 102, DateTimeKind.Local).AddTicks(1884), new DateTime(2024, 5, 31, 14, 18, 29, 102, DateTimeKind.Local).AddTicks(1885) });
        }
    }
}
