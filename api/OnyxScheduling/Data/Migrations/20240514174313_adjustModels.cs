using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class adjustModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 14, 13, 43, 13, 219, DateTimeKind.Local).AddTicks(3673), new DateTime(2024, 5, 14, 13, 43, 13, 219, DateTimeKind.Local).AddTicks(3715) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 14, 13, 43, 13, 219, DateTimeKind.Local).AddTicks(3721), new DateTime(2024, 5, 14, 13, 43, 13, 219, DateTimeKind.Local).AddTicks(3722) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 14, 13, 38, 57, 776, DateTimeKind.Local).AddTicks(7040), new DateTime(2024, 5, 14, 13, 38, 57, 776, DateTimeKind.Local).AddTicks(7083) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 14, 13, 38, 57, 776, DateTimeKind.Local).AddTicks(7088), new DateTime(2024, 5, 14, 13, 38, 57, 776, DateTimeKind.Local).AddTicks(7089) });
        }
    }
}
