using Microsoft.EntityFrameworkCore.Migrations;

namespace OnyxScheduling.Data.Migrations
{
    public partial class AddJObId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Invoices");
        }
    }
}