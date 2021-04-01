using Microsoft.EntityFrameworkCore.Migrations;

namespace LloydStephanieRealty.Migrations
{
    public partial class FixedSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Comments_CommentsID",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CommentsID",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CommentsID",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogID",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogID",
                table: "Comments",
                column: "BlogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogID",
                table: "Comments",
                column: "BlogID",
                principalTable: "Blogs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BlogID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BlogID",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CommentsID",
                table: "Blogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CommentsID",
                table: "Blogs",
                column: "CommentsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Comments_CommentsID",
                table: "Blogs",
                column: "CommentsID",
                principalTable: "Comments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
