using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblItem_tblItem_ItemId",
                table: "tblItem");

            migrationBuilder.DropForeignKey(
                name: "FK_tblItemRequest_AspNetUsers_OwnerId",
                table: "tblItemRequest");

            migrationBuilder.DropIndex(
                name: "IX_tblItemRequest_OwnerId",
                table: "tblItemRequest");

            migrationBuilder.DropIndex(
                name: "IX_tblItem_ItemId",
                table: "tblItem");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "tblItemRequest");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "tblItem");

            migrationBuilder.AddColumn<int>(
                name: "Credit",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "tblItemRequest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "tblItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblItemRequest_OwnerId",
                table: "tblItemRequest",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_ItemId",
                table: "tblItem",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblItem_tblItem_ItemId",
                table: "tblItem",
                column: "ItemId",
                principalTable: "tblItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblItemRequest_AspNetUsers_OwnerId",
                table: "tblItemRequest",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
