using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class FilePathToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Invoices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FilePath", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 30, 16, 58, 39, 375, DateTimeKind.Local).AddTicks(2523), null, new DateTime(2024, 5, 30, 16, 58, 39, 375, DateTimeKind.Local).AddTicks(2566) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FilePath", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 30, 16, 58, 39, 375, DateTimeKind.Local).AddTicks(2571), null, new DateTime(2024, 5, 30, 16, 58, 39, 375, DateTimeKind.Local).AddTicks(2572) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Invoices");

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
    }
}
