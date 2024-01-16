using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNullableFinishedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishedDateTime",
                table: "Invoices",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 1, 15, 22, 8, 33, 423, DateTimeKind.Local).AddTicks(5327), new DateTime(2024, 1, 15, 22, 8, 33, 423, DateTimeKind.Local).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 1, 15, 22, 8, 33, 423, DateTimeKind.Local).AddTicks(5375), new DateTime(2024, 1, 15, 22, 8, 33, 423, DateTimeKind.Local).AddTicks(5376) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishedDateTime",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 1, 14, 22, 27, 4, 424, DateTimeKind.Local).AddTicks(2100), new DateTime(2024, 1, 14, 22, 27, 4, 424, DateTimeKind.Local).AddTicks(2148) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 1, 14, 22, 27, 4, 424, DateTimeKind.Local).AddTicks(2151), new DateTime(2024, 1, 14, 22, 27, 4, 424, DateTimeKind.Local).AddTicks(2153) });
        }
    }
}
