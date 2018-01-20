using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBook_tblStudent_StudentId",
                table: "tblBook");

            migrationBuilder.DropIndex(
                name: "IX_tblBook_StudentId",
                table: "tblBook");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "tblBook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "tblBook",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBook_StudentId",
                table: "tblBook",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBook_tblStudent_StudentId",
                table: "tblBook",
                column: "StudentId",
                principalTable: "tblStudent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
