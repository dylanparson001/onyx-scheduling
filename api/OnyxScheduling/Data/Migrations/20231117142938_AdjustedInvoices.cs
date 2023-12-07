using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnyxScheduling.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedInvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customer_Assigned_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Technicians_Assigned_TechnicianId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Assigned_TechnicianId",
                table: "Invoices",
                newName: "TechniciansId");

            migrationBuilder.RenameColumn(
                name: "Assigned_CustomerId",
                table: "Invoices",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_Assigned_TechnicianId",
                table: "Invoices",
                newName: "IX_Invoices_TechniciansId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_Assigned_CustomerId",
                table: "Invoices",
                newName: "IX_Invoices_CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "Assigned_Customer_Id",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Assigned_Technician_Id",
                table: "Invoices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Total_Price",
                table: "Invoices",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customer_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Technicians_TechniciansId",
                table: "Invoices",
                column: "TechniciansId",
                principalTable: "Technicians",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customer_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Technicians_TechniciansId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Assigned_Customer_Id",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Assigned_Technician_Id",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Total_Price",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "TechniciansId",
                table: "Invoices",
                newName: "Assigned_TechnicianId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Invoices",
                newName: "Assigned_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_TechniciansId",
                table: "Invoices",
                newName: "IX_Invoices_Assigned_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                newName: "IX_Invoices_Assigned_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customer_Assigned_CustomerId",
                table: "Invoices",
                column: "Assigned_CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Technicians_Assigned_TechnicianId",
                table: "Invoices",
                column: "Assigned_TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id");
        }
    }
}
