using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblItemRecord_AspNetUsers_OwnerId",
                table: "tblItemRecord");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "tblItemRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblItemRecord_AspNetUsers_OwnerId",
                table: "tblItemRecord",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblItemRecord_AspNetUsers_OwnerId",
                table: "tblItemRecord");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "tblItemRecord",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_tblItemRecord_AspNetUsers_OwnerId",
                table: "tblItemRecord",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
