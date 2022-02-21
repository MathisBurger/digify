using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digify.Migrations
{
    public partial class add_subject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "TimeTableElements",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "TimeTableElements");
        }
    }
}
