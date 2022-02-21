using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digify.Migrations
{
    public partial class foreign_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Timetables_Id",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Timetables_Users_Id",
                table: "Timetables",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timetables_Users_Id",
                table: "Timetables");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Timetables_Id",
                table: "Users",
                column: "Id",
                principalTable: "Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
