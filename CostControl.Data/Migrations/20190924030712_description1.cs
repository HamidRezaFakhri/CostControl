using Microsoft.EntityFrameworkCore.Migrations;

namespace CostControl.Data.Migrations
{
    public partial class description1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripton",
                schema: "dbo",
                table: "IntakeRemittanceItemLog",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "dbo",
                table: "IntakeRemittanceItemLog",
                newName: "Descripton");
        }
    }
}
