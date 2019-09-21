using Microsoft.EntityFrameworkCore.Migrations;

namespace CostControl.Data.Migrations
{
    public partial class Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripton",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                newName: "Descripton");
        }
    }
}
