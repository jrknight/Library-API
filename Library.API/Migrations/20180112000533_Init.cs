using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAuthor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGenre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGenre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSchool",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admin = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    SchoolName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSchool", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblStudent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStudent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    ISBN = table.Column<string>(nullable: false),
                    StudentId = table.Column<int>(nullable: true),
                    Summary = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBook_tblAuthor_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "tblAuthor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBook_tblStudent_StudentId",
                        column: x => x.StudentId,
                        principalTable: "tblStudent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblBookGenre",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBookGenre", x => new { x.BookId, x.GenreId });
                    table.UniqueConstraint("AK_tblBookGenre_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBookGenre_tblBook_BookId",
                        column: x => x.BookId,
                        principalTable: "tblBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBookGenre_tblGenre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "tblGenre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBook_AuthorId",
                table: "tblBook",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBook_StudentId",
                table: "tblBook",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookGenre_GenreId",
                table: "tblBookGenre",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBookGenre");

            migrationBuilder.DropTable(
                name: "tblSchool");

            migrationBuilder.DropTable(
                name: "tblBook");

            migrationBuilder.DropTable(
                name: "tblGenre");

            migrationBuilder.DropTable(
                name: "tblAuthor");

            migrationBuilder.DropTable(
                name: "tblStudent");
        }
    }
}
