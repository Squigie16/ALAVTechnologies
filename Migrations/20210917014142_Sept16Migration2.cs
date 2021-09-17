using Microsoft.EntityFrameworkCore.Migrations;

namespace LloydStephanieRealty.Migrations
{
    public partial class Sept16Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "MailingListUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "MailingListUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "MailingListUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "MailingListUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "MailingListUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "MailingListUsers");
        }
    }
}
