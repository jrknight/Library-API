using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class userstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblItem_AspNetUsers_UserId",
                table: "tblItem");

            migrationBuilder.DropForeignKey(
                name: "FK_tblItemRecord_AspNetUsers_OwnerId",
                table: "tblItemRecord");

            migrationBuilder.DropIndex(
                name: "IX_tblItemRecord_OwnerId",
                table: "tblItemRecord");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "tblItemRecord");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tblItem",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_tblItem_UserId",
                table: "tblItem",
                newName: "IX_tblItem_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblItem_AspNetUsers_OwnerId",
                table: "tblItem",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblItem_AspNetUsers_OwnerId",
                table: "tblItem");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "tblItem",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tblItem_OwnerId",
                table: "tblItem",
                newName: "IX_tblItem_UserId");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "tblItemRecord",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblItemRecord_OwnerId",
                table: "tblItemRecord",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblItem_AspNetUsers_UserId",
                table: "tblItem",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
