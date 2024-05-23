using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Invoice_Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 123,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Invoice_Items",
                keyColumn: "Id",
                keyValue: 234,
                column: "Quantity",
                value: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Invoice_Items");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 23, 10, 52, 45, 829, DateTimeKind.Local).AddTicks(562), new DateTime(2024, 5, 23, 10, 52, 45, 829, DateTimeKind.Local).AddTicks(604) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 5, 23, 10, 52, 45, 829, DateTimeKind.Local).AddTicks(609), new DateTime(2024, 5, 23, 10, 52, 45, 829, DateTimeKind.Local).AddTicks(610) });
        }
    }
}
