using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStartEndScheduleTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledEndDateTime",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledStartDateTime",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime", "Processing_Status", "ScheduledEndDateTime", "ScheduledStartDateTime" },
                values: new object[] { new DateTime(2024, 1, 8, 15, 50, 6, 694, DateTimeKind.Local).AddTicks(1411), new DateTime(2024, 1, 8, 15, 50, 6, 694, DateTimeKind.Local).AddTicks(1451), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime", "Processing_Status", "ScheduledEndDateTime", "ScheduledStartDateTime" },
                values: new object[] { new DateTime(2024, 1, 8, 15, 50, 6, 694, DateTimeKind.Local).AddTicks(1454), new DateTime(2024, 1, 8, 15, 50, 6, 694, DateTimeKind.Local).AddTicks(1455), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledEndDateTime",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ScheduledStartDateTime",
                table: "Invoices");

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
                values: new object[] { new DateTime(2024, 1, 7, 18, 35, 44, 411, DateTimeKind.Local).AddTicks(6478), new DateTime(2024, 1, 7, 18, 35, 44, 411, DateTimeKind.Local).AddTicks(6527), null });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime", "Processing_Status" },
                values: new object[] { new DateTime(2024, 1, 7, 18, 35, 44, 411, DateTimeKind.Local).AddTicks(6531), new DateTime(2024, 1, 7, 18, 35, 44, 411, DateTimeKind.Local).AddTicks(6533), null });
        }
    }
}
