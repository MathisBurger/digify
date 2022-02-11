using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digify.Migrations
{
    public partial class fixCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classes_SchoolClassId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classes_SchoolClassId",
                table: "Users",
                column: "SchoolClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classes_SchoolClassId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classes_SchoolClassId",
                table: "Users",
                column: "SchoolClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
