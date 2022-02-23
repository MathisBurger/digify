using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digify.Migrations
{
    public partial class classbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classbook",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<string>(type: "text", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false),
                    ArchivedName = table.Column<string>(type: "text", nullable: true),
                    ArchivedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classbook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classbook_Classes_Id",
                        column: x => x.Id,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassbookDayEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentClassbookId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassbookDayEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassbookDayEntry_Classbook_ParentClassbookId",
                        column: x => x.ParentClassbookId,
                        principalTable: "Classbook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassbookDayEntryLesson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentDayEntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubjectColor = table.Column<string>(type: "text", nullable: false),
                    ApprovedByTeacher = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassbookDayEntryLesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassbookDayEntryLesson_ClassbookDayEntry_ParentDayEntryId",
                        column: x => x.ParentDayEntryId,
                        principalTable: "ClassbookDayEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassbookDayEntryUser",
                columns: table => new
                {
                    MissedDaysId = table.Column<Guid>(type: "uuid", nullable: false),
                    MissingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassbookDayEntryUser", x => new { x.MissedDaysId, x.MissingId });
                    table.ForeignKey(
                        name: "FK_ClassbookDayEntryUser_ClassbookDayEntry_MissedDaysId",
                        column: x => x.MissedDaysId,
                        principalTable: "ClassbookDayEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassbookDayEntryUser_Users_MissingId",
                        column: x => x.MissingId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassbookDayEntry_ParentClassbookId",
                table: "ClassbookDayEntry",
                column: "ParentClassbookId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassbookDayEntryLesson_ParentDayEntryId",
                table: "ClassbookDayEntryLesson",
                column: "ParentDayEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassbookDayEntryUser_MissingId",
                table: "ClassbookDayEntryUser",
                column: "MissingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassbookDayEntryLesson");

            migrationBuilder.DropTable(
                name: "ClassbookDayEntryUser");

            migrationBuilder.DropTable(
                name: "ClassbookDayEntry");

            migrationBuilder.DropTable(
                name: "Classbook");
        }
    }
}
