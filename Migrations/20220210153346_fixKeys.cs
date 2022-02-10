using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digify.Migrations
{
    public partial class fixKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassUser_Classes_Id",
                table: "ClassUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassUser_Users_Id",
                table: "ClassUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classes_Id",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassUser",
                table: "ClassUser");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ClassUser",
                newName: "TeachersId");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolClassId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClassesId",
                table: "ClassUser",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassUser",
                table: "ClassUser",
                columns: new[] { "ClassesId", "TeachersId" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolClassId",
                table: "Users",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassUser_TeachersId",
                table: "ClassUser",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassUser_Classes_ClassesId",
                table: "ClassUser",
                column: "ClassesId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassUser_Users_TeachersId",
                table: "ClassUser",
                column: "TeachersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classes_SchoolClassId",
                table: "Users",
                column: "SchoolClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassUser_Classes_ClassesId",
                table: "ClassUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassUser_Users_TeachersId",
                table: "ClassUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classes_SchoolClassId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SchoolClassId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassUser",
                table: "ClassUser");

            migrationBuilder.DropIndex(
                name: "IX_ClassUser_TeachersId",
                table: "ClassUser");

            migrationBuilder.DropColumn(
                name: "SchoolClassId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "ClassUser");

            migrationBuilder.RenameColumn(
                name: "TeachersId",
                table: "ClassUser",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassUser",
                table: "ClassUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassUser_Classes_Id",
                table: "ClassUser",
                column: "Id",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassUser_Users_Id",
                table: "ClassUser",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classes_Id",
                table: "Users",
                column: "Id",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
