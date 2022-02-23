using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digify.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classbooks",
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
                    table.PrimaryKey("PK_Classbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classbooks_Classes_Id",
                        column: x => x.Id,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Roles = table.Column<string[]>(type: "text[]", nullable: false),
                    SchoolClassId = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Classes_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ClassbookDayEntries",
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
                    table.PrimaryKey("PK_ClassbookDayEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassbookDayEntries_Classbooks_ParentClassbookId",
                        column: x => x.ParentClassbookId,
                        principalTable: "Classbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassUser",
                columns: table => new
                {
                    ClassesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeachersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassUser", x => new { x.ClassesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_ClassUser_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassUser_Users_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timetables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timetables_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassbookDayEntryLessons",
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
                    table.PrimaryKey("PK_ClassbookDayEntryLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassbookDayEntryLessons_ClassbookDayEntries_ParentDayEntry~",
                        column: x => x.ParentDayEntryId,
                        principalTable: "ClassbookDayEntries",
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
                        name: "FK_ClassbookDayEntryUser_ClassbookDayEntries_MissedDaysId",
                        column: x => x.MissedDaysId,
                        principalTable: "ClassbookDayEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassbookDayEntryUser_Users_MissingId",
                        column: x => x.MissingId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeTableElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Teacher = table.Column<string>(type: "text", nullable: false),
                    Room = table.Column<string>(type: "text", nullable: false),
                    SubjectColor = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTableElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeTableElements_Timetables_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Timetables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassbookDayEntries_ParentClassbookId",
                table: "ClassbookDayEntries",
                column: "ParentClassbookId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassbookDayEntryLessons_ParentDayEntryId",
                table: "ClassbookDayEntryLessons",
                column: "ParentDayEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassbookDayEntryUser_MissingId",
                table: "ClassbookDayEntryUser",
                column: "MissingId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassUser_TeachersId",
                table: "ClassUser",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTableElements_ParentId",
                table: "TimeTableElements",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolClassId",
                table: "Users",
                column: "SchoolClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassbookDayEntryLessons");

            migrationBuilder.DropTable(
                name: "ClassbookDayEntryUser");

            migrationBuilder.DropTable(
                name: "ClassUser");

            migrationBuilder.DropTable(
                name: "TimeTableElements");

            migrationBuilder.DropTable(
                name: "ClassbookDayEntries");

            migrationBuilder.DropTable(
                name: "Timetables");

            migrationBuilder.DropTable(
                name: "Classbooks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
