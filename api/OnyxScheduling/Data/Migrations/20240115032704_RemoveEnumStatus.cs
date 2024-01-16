using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEnumStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Processing_Status",
                table: "Invoices",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime", "Processing_Status" },
                values: new object[] { new DateTime(2024, 1, 14, 22, 27, 4, 424, DateTimeKind.Local).AddTicks(2100), new DateTime(2024, 1, 14, 22, 27, 4, 424, DateTimeKind.Local).AddTicks(2148), null });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime", "Processing_Status" },
                values: new object[] { new DateTime(2024, 1, 14, 22, 27, 4, 424, DateTimeKind.Local).AddTicks(2151), new DateTime(2024, 1, 14, 22, 27, 4, 424, DateTimeKind.Local).AddTicks(2153), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Processing_Status",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime", "Processing_Status" },
                values: new object[] { new DateTime(2024, 1, 8, 15, 50, 6, 694, DateTimeKind.Local).AddTicks(1411), new DateTime(2024, 1, 8, 15, 50, 6, 694, DateTimeKind.Local).AddTicks(1451), 0 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime", "Processing_Status" },
                values: new object[] { new DateTime(2024, 1, 8, 15, 50, 6, 694, DateTimeKind.Local).AddTicks(1454), new DateTime(2024, 1, 8, 15, 50, 6, 694, DateTimeKind.Local).AddTicks(1455), 0 });
        }
    }
}
