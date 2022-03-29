using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digify.Migrations
{
    public partial class teacher_timetable_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teacher",
                table: "TimeTableElements");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "TimeTableElements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "ClassbookDayEntryLessons",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTableElements_TeacherId",
                table: "TimeTableElements",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassbookDayEntryLessons_TeacherId",
                table: "ClassbookDayEntryLessons",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassbookDayEntryLessons_Users_TeacherId",
                table: "ClassbookDayEntryLessons",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTableElements_Users_TeacherId",
                table: "TimeTableElements",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassbookDayEntryLessons_Users_TeacherId",
                table: "ClassbookDayEntryLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTableElements_Users_TeacherId",
                table: "TimeTableElements");

            migrationBuilder.DropIndex(
                name: "IX_TimeTableElements_TeacherId",
                table: "TimeTableElements");

            migrationBuilder.DropIndex(
                name: "IX_ClassbookDayEntryLessons_TeacherId",
                table: "ClassbookDayEntryLessons");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "TimeTableElements");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "ClassbookDayEntryLessons");

            migrationBuilder.AddColumn<string>(
                name: "Teacher",
                table: "TimeTableElements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
