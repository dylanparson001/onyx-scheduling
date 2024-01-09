using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedAddressToUSers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 1, 7, 18, 35, 44, 411, DateTimeKind.Local).AddTicks(6478), new DateTime(2024, 1, 7, 18, 35, 44, 411, DateTimeKind.Local).AddTicks(6527) });
            migrationBuilder.AddColumn<string>(
          name: "Address",
          table: "AspNetUsers",
          type: "TEXT",
          nullable: true,
          defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 1, 7, 18, 35, 44, 411, DateTimeKind.Local).AddTicks(6531), new DateTime(2024, 1, 7, 18, 35, 44, 411, DateTimeKind.Local).AddTicks(6533) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 1, 6, 15, 39, 35, 549, DateTimeKind.Local).AddTicks(3983), new DateTime(2024, 1, 6, 15, 39, 35, 549, DateTimeKind.Local).AddTicks(4038) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 1, 6, 15, 39, 35, 549, DateTimeKind.Local).AddTicks(4042), new DateTime(2024, 1, 6, 15, 39, 35, 549, DateTimeKind.Local).AddTicks(4044) });
        }
    }
}
