using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LloydStephanieRealty.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Images_ImageId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ImageId",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Blogs",
                newName: "ImageID");

            migrationBuilder.AlterColumn<int>(
                name: "ImageID",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Testimonies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfPost = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonies", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Testimonies");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "Blogs",
                newName: "ImageId");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Blogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ImageId",
                table: "Blogs",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Images_ImageId",
                table: "Blogs",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
