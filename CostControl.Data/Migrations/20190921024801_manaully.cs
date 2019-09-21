using Microsoft.EntityFrameworkCore.Migrations;

namespace CostControl.Data.Migrations
{
    public partial class manaully : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAddedManualy",
                schema: "dbo",
                table: "IntakeRemittanceItemLog",
                newName: "IsAddedManually");

            migrationBuilder.RenameColumn(
                name: "IsAddedManualy",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                newName: "IsAddedManually");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAddedManually",
                schema: "dbo",
                table: "IntakeRemittanceItemLog",
                newName: "IsAddedManualy");

            migrationBuilder.RenameColumn(
                name: "IsAddedManually",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                newName: "IsAddedManualy");
        }
    }
}
