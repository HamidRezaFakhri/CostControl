using Microsoft.EntityFrameworkCore.Migrations;

namespace CostControl.Data.Migrations
{
    public partial class IsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAddedManualy",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAddedManualy",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "0");
        }
    }
}
