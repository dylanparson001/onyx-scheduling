using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class JobInvoiceItems : Migration
    {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                // Create the JobInvoice_Item table
                migrationBuilder.CreateTable(
                    name: "JobInvoice_Item",
                    columns: table => new
                    {
                        JobId = table.Column<int>(type: "INTEGER", nullable: false),
                        InvoiceItemId = table.Column<int>(type: "INTEGER", nullable: false),
                        Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_JobInvoice_Item", x => new { x.JobId, x.InvoiceItemId });
                        table.ForeignKey(
                            name: "FK_JobInvoice_Item_Jobs_JobId",
                            column: x => x.JobId,
                            principalTable: "Jobs",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_JobInvoice_Item_Invoice_Items_InvoiceItemId",
                            column: x => x.InvoiceItemId,
                            principalTable: "Invoice_Items",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                // Create the index for InvoiceItemId
                migrationBuilder.CreateIndex(
                    name: "IX_JobInvoice_Item_InvoiceItemId",
                    table: "JobInvoice_Item",
                    column: "InvoiceItemId");

                // Update data in the Invoices table
                migrationBuilder.UpdateData(
                    table: "Invoices",
                    keyColumn: "Id",
                    keyValue: 1,
                    columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                    values: new object[]
                    {
                        new DateTime(2024, 5, 23, 10, 52, 45, 829, DateTimeKind.Local).AddTicks(562),
                        new DateTime(2024, 5, 23, 10, 52, 45, 829, DateTimeKind.Local).AddTicks(604)
                    });

                migrationBuilder.UpdateData(
                    table: "Invoices",
                    keyColumn: "Id",
                    keyValue: 2,
                    columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                    values: new object[]
                    {
                        new DateTime(2024, 5, 23, 10, 52, 45, 829, DateTimeKind.Local).AddTicks(609),
                        new DateTime(2024, 5, 23, 10, 52, 45, 829, DateTimeKind.Local).AddTicks(610)
                    });
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                // Remove the foreign keys
                migrationBuilder.DropForeignKey(
                    name: "FK_JobInvoice_Item_Jobs_JobId",
                    table: "JobInvoice_Item");

                migrationBuilder.DropForeignKey(
                    name: "FK_JobInvoice_Item_Invoice_Items_InvoiceItemId",
                    table: "JobInvoice_Item");

                // Drop the index for InvoiceItemId
                migrationBuilder.DropIndex(
                    name: "IX_JobInvoice_Item_InvoiceItemId",
                    table: "JobInvoice_Item");

                // Drop the JobInvoice_Item table
                migrationBuilder.DropTable(
                    name: "JobInvoice_Item");

                // Update data in the Invoices table to revert changes
                migrationBuilder.UpdateData(
                    table: "Invoices",
                    keyColumn: "Id",
                    keyValue: 1,
                    columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                    values: new object[]
                    {
                        new DateTime(2024, 5, 23, 10, 48, 13, 969, DateTimeKind.Local).AddTicks(8665),
                        new DateTime(2024, 5, 23, 10, 48, 13, 969, DateTimeKind.Local).AddTicks(8710)
                    });

                migrationBuilder.UpdateData(
                    table: "Invoices",
                    keyColumn: "Id",
                    keyValue: 2,
                    columns: new[] { "CreatedDateTime", "FinishedDateTime" },
                    values: new object[]
                    {
                        new DateTime(2024, 5, 23, 10, 48, 13, 969, DateTimeKind.Local).AddTicks(8715),
                        new DateTime(2024, 5, 23, 10, 48, 13, 969, DateTimeKind.Local).AddTicks(8716)
                    });
            }
        }
    }
