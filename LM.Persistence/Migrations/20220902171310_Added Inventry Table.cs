using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LM.Persistence.Migrations
{
    public partial class AddedInventryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Bookss",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "BookHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "BookGenres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookInventories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInventories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookInventories_Bookss_BookId",
                        column: x => x.BookId,
                        principalTable: "Bookss",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookInventories_BookId",
                table: "BookInventories",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookInventories");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Bookss");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "BookHistories");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "BookGenres");
        }
    }
}
