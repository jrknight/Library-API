using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.API.Migrations
{
    public partial class BetterBookRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBookRequest_tblStudent_StudentId",
                table: "tblBookRequest");

            migrationBuilder.DropIndex(
                name: "IX_tblBookRequest_StudentId",
                table: "tblBookRequest");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "tblBookRequest");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "tblBookRequest",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookRequest_UserId",
                table: "tblBookRequest",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBookRequest_AspNetUsers_UserId",
                table: "tblBookRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBookRequest_AspNetUsers_UserId",
                table: "tblBookRequest");

            migrationBuilder.DropIndex(
                name: "IX_tblBookRequest_UserId",
                table: "tblBookRequest");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblBookRequest");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "tblBookRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblBookRequest_StudentId",
                table: "tblBookRequest",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBookRequest_tblStudent_StudentId",
                table: "tblBookRequest",
                column: "StudentId",
                principalTable: "tblStudent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
