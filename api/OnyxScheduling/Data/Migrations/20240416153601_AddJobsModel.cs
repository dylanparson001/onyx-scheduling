using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJobsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobsId",
                table: "InvoiceInvoice_Item",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Processing_Status = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ScheduledStartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ScheduledEndDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Assigned_Technician_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Assigned_Customer_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Total_Price = table.Column<double>(type: "REAL", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 1, 123 },
                column: "JobsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 2, 123 },
                column: "JobsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "InvoiceInvoice_Item",
                keyColumns: new[] { "InvoiceId", "InvoiceItemId" },
                keyValues: new object[] { 2, 234 },
                column: "JobsId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 4, 16, 11, 36, 1, 464, DateTimeKind.Local).AddTicks(7339), new DateTime(2024, 4, 16, 11, 36, 1, 464, DateTimeKind.Local).AddTicks(7378) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                values: new object[] { new DateTime(2024, 4, 16, 11, 36, 1, 464, DateTimeKind.Local).AddTicks(7383), new DateTime(2024, 4, 16, 11, 36, 1, 464, DateTimeKind.Local).AddTicks(7384) });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceInvoice_Item_JobsId",
                table: "InvoiceInvoice_Item",
                column: "JobsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceInvoice_Item_Jobs_JobsId",
                table: "InvoiceInvoice_Item",
                column: "JobsId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceInvoice_Item_Jobs_JobsId",
                table: "InvoiceInvoice_Item");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceInvoice_Item_JobsId",
                table: "InvoiceInvoice_Item");

            migrationBuilder.DropColumn(
                name: "JobsId",
                table: "InvoiceInvoice_Item");

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
    }
}
