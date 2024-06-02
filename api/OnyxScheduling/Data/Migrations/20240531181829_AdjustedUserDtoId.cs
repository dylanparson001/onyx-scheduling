using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedUserDtoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 30, 16, 58, 39, 375, DateTimeKind.Local).AddTicks(2523), new DateTime(2024, 5, 30, 16, 58, 39, 375, DateTimeKind.Local).AddTicks(2566) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 30, 16, 58, 39, 375, DateTimeKind.Local).AddTicks(2571), new DateTime(2024, 5, 30, 16, 58, 39, 375, DateTimeKind.Local).AddTicks(2572) });
        }
    }
}
