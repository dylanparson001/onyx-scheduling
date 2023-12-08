using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIdTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Assigned_Customer_Id", "Assigned_Technician_Id", "CreatedDateTime" },
                values: new object[] { "01c84c0a-84f1-4504-94ba-ce28a4c99245", "2", new DateTime(2023, 12, 8, 11, 12, 45, 441, DateTimeKind.Local).AddTicks(5070) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Assigned_Customer_Id", "Assigned_Technician_Id", "CreatedDateTime" },
                values: new object[] { "01c84c0a-84f1-4504-94ba-ce28a4c99245", "2", new DateTime(2023, 12, 8, 11, 12, 45, 441, DateTimeKind.Local).AddTicks(5110) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          


            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Assigned_Customer_Id", "Assigned_Technician_Id", "CreatedDateTime", "TechniciansId" },
                values: new object[] { 1, 2, new DateTime(2023, 11, 20, 12, 20, 15, 559, DateTimeKind.Local).AddTicks(6715), null });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Assigned_Customer_Id", "Assigned_Technician_Id", "CreatedDateTime", "TechniciansId" },
                values: new object[] { 1, 2, new DateTime(2023, 11, 20, 12, 20, 15, 559, DateTimeKind.Local).AddTicks(6756), null });

        }
    }
}
