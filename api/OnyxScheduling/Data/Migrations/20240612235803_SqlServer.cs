using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class SqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice_Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Id = table.Column<int>(type: "int", nullable: false),
                    Item_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DefaultQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processing_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduledStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledEndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assigned_Technician_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assigned_Customer_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Price = table.Column<double>(type: "float", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processing_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduledStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledEndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assigned_Technician_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assigned_Customer_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Price = table.Column<double>(type: "float", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceInvoice_Item",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    InvoiceItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceInvoice_Item", x => new { x.InvoiceId, x.InvoiceItemId });
                    table.ForeignKey(
                        name: "FK_InvoiceInvoice_Item_Invoice_Items_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "Invoice_Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceInvoice_Item_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobInvoice_Item",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    InvoiceItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobInvoice_Item", x => new { x.JobId, x.InvoiceItemId });
                    table.ForeignKey(
                        name: "FK_JobInvoice_Item_Invoice_Items_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "Invoice_Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobInvoice_Item_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceInvoice_Item_InvoiceItemId",
                table: "InvoiceInvoice_Item",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_JobInvoice_Item_InvoiceItemId",
                table: "JobInvoice_Item",
                column: "InvoiceItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "InvoiceInvoice_Item");

            migrationBuilder.DropTable(
                name: "JobInvoice_Item");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Invoice_Items");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
