using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostControl.Data.Migrations
{
    public partial class log : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LogDate",
                schema: "dbo",
                table: "IntakeRemittanceItemLog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LogUserId",
                schema: "dbo",
                table: "IntakeRemittanceItemLog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IntakeRemittanceItemLog_LogUserId",
                schema: "dbo",
                table: "IntakeRemittanceItemLog",
                column: "LogUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeRemittanceItemLog_IncommingUser_LogUserId",
                schema: "dbo",
                table: "IntakeRemittanceItemLog",
                column: "LogUserId",
                principalSchema: "dbo",
                principalTable: "IncommingUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeRemittanceItemLog_IncommingUser_LogUserId",
                schema: "dbo",
                table: "IntakeRemittanceItemLog");

            migrationBuilder.DropIndex(
                name: "IX_IntakeRemittanceItemLog_LogUserId",
                schema: "dbo",
                table: "IntakeRemittanceItemLog");

            migrationBuilder.DropColumn(
                name: "LogDate",
                schema: "dbo",
                table: "IntakeRemittanceItemLog");

            migrationBuilder.DropColumn(
                name: "LogUserId",
                schema: "dbo",
                table: "IntakeRemittanceItemLog");
        }
    }
}
