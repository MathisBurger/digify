using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digify.Migrations
{
    public partial class addTimetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTableElements_Timetables_Id",
                table: "TimeTableElements");

            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_Users_Id",
                table: "Timetables");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "TimeTableElements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TimeTableElements_ParentId",
                table: "TimeTableElements",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTableElements_Timetables_ParentId",
                table: "TimeTableElements",
                column: "ParentId",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Timetables_Id",
                table: "Users",
                column: "Id",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTableElements_Timetables_ParentId",
                table: "TimeTableElements");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Timetables_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TimeTableElements_ParentId",
                table: "TimeTableElements");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "TimeTableElements");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTableElements_Timetables_Id",
                table: "TimeTableElements",
                column: "Id",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_Users_Id",
                table: "Timetables",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
