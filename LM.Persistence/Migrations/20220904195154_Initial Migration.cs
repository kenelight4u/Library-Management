using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LM.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordResetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BookGenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Book_BookGenres_BookGenresId",
                        column: x => x.BookGenresId,
                        principalTable: "BookGenres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookHistories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LMUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BorrowStatus = table.Column<int>(type: "int", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookHistories_AspNetUsers_LMUserId",
                        column: x => x.LMUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookHistories_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookInventory",
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
                    table.PrimaryKey("PK_BookInventory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookInventory_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "ID", "CreationTime", "CreatorUserId", "Description", "LastModificationTime", "Name" },
                values: new object[] { new Guid("d6b44d21-d94b-4716-acaa-f26a6328fa0b"), new DateTime(2022, 9, 4, 20, 51, 51, 623, DateTimeKind.Local).AddTicks(490), "473c9df0-2634-4506-ad6c-9b98336082f2", "These books are based in a time period set in the past decades, often against the backdrop of significant (real) historical events.", null, "Historical Fiction" });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "ID", "CreationTime", "CreatorUserId", "Description", "LastModificationTime", "Name" },
                values: new object[] { new Guid("8bea3613-18a2-4bd6-a8a3-6e34ce4881f3"), new DateTime(2022, 9, 4, 20, 51, 51, 623, DateTimeKind.Local).AddTicks(4360), "473c9df0-2634-4506-ad6c-9b98336082f2", "The plot always revolves around a crime of sorts that must be solved—or foiled—by the protagonists.", null, "Detective and Mystery" });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "ID", "CreationTime", "CreatorUserId", "Description", "LastModificationTime", "Name" },
                values: new object[] { new Guid("f07f8895-b747-459d-ceb1-85f77c5e7d67"), new DateTime(2022, 9, 4, 20, 51, 51, 623, DateTimeKind.Local).AddTicks(4378), "473c9df0-2634-4506-ad6c-9b98336082f2", "The stories in comic books and graphic novels are presented to the reader through engaging, sequential narrative art (illustrations and typography) that's either presented in a specific design or the traditional panel layout you find in comics. With both, you'll often find the dialogue presented in the tell-tale \"word balloons\" next to the respective characters.", null, "Comic Book or Graphic Novel" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "ID", "Author", "BookGenresId", "CreationTime", "CreatorUserId", "Description", "ISBN", "LastModificationTime", "Quantity", "Title" },
                values: new object[] { new Guid("5ee58416-76d4-4bad-852e-dc2ed486e482"), " Gabriel Garcia Marquez ", new Guid("d6b44d21-d94b-4716-acaa-f26a6328fa0b"), new DateTime(2022, 9, 4, 20, 51, 51, 625, DateTimeKind.Local).AddTicks(9076), "473c9df0-2634-4506-ad6c-9b98336082f2", "One Hundred Years of Solitude is the first piece of literature since the Book of Genesis that should be required reading for the entire human race. . . . Mr. Garcia Marquez has done nothing less than to create in the reader a sense of all that is profound, meaningful, and meaningless in life.", "978-3-16-148410-0", null, 4, "One Hundred Years of Solitude" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "ID", "Author", "BookGenresId", "CreationTime", "CreatorUserId", "Description", "ISBN", "LastModificationTime", "Quantity", "Title" },
                values: new object[] { new Guid("7969a9ac-c081-4b75-a7fc-51e64c79c8dc"), "Alan Moore", new Guid("8bea3613-18a2-4bd6-a8a3-6e34ce4881f3"), new DateTime(2022, 9, 4, 20, 51, 51, 625, DateTimeKind.Local).AddTicks(9113), "473c9df0-2634-4506-ad6c-9b98336082f2", "A hit HBO original series, Watchmen, the groundbreaking series from award-winning author Alan Moore, presents a world where the mere presence of American superheroes changed history--the U.S. won the Vietnam War, Nixon is still president, and the Cold War is in full effect.", "978-3-16-148410-1", null, 2, "Watchmen " });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "ID", "Author", "BookGenresId", "CreationTime", "CreatorUserId", "Description", "ISBN", "LastModificationTime", "Quantity", "Title" },
                values: new object[] { new Guid("13088fc2-c0ee-487e-91a1-2b977214d0a1"), "Alan Moore", new Guid("f07f8895-b747-459d-ceb1-85f77c5e7d67"), new DateTime(2022, 9, 4, 20, 51, 51, 625, DateTimeKind.Local).AddTicks(9117), "473c9df0-2634-4506-ad6c-9b98336082f2", "After the sinking of a cargo ship, a solitary lifeboat remains bobbing on the wild blue Pacific. The only survivors from the wreck are a sixteen-year-old boy named Pi, a hyena, a wounded zebra, an orangutan—and a 450-pound Royal Bengal tiger.\r\n\r\nSoon the tiger has dispatched all but Pi Patel, whose fear, knowledge, and cunning allow him to coexist with the tiger, Richard Parker, for 227 days while lost at sea. When they finally reach the coast of Mexico, Richard Parker flees to the jungle, never to be seen again. The Japanese authorities who interrogate Pi refuse to believe his story and press him to tell them \"the truth.\" After hours of coercion, Pi tells a second story, a story much less fantastical, much more conventional—but is it more true?\r\n\r\n\"A story to make you believe in the soul-sustaining power of fiction.\"—Los Angeles Times Book Review\r\n", "978-3-16-148410-2", null, 3, "Life Of Pi Paperback" });

            migrationBuilder.InsertData(
                table: "BookInventory",
                columns: new[] { "ID", "BookId", "CreationTime", "CreatorUserId", "LastModificationTime", "Quantity" },
                values: new object[] { new Guid("8ec05cfa-11e5-4f32-b732-dbd3a953b20b"), new Guid("5ee58416-76d4-4bad-852e-dc2ed486e482"), new DateTime(2022, 9, 4, 20, 51, 51, 624, DateTimeKind.Local).AddTicks(8661), "473c9df0-2634-4506-ad6c-9b98336082f2", null, 4 });

            migrationBuilder.InsertData(
                table: "BookInventory",
                columns: new[] { "ID", "BookId", "CreationTime", "CreatorUserId", "LastModificationTime", "Quantity" },
                values: new object[] { new Guid("536a1a94-c132-4163-bee5-2d0e1986882a"), new Guid("7969a9ac-c081-4b75-a7fc-51e64c79c8dc"), new DateTime(2022, 9, 4, 20, 51, 51, 624, DateTimeKind.Local).AddTicks(8698), "473c9df0-2634-4506-ad6c-9b98336082f2", null, 2 });

            migrationBuilder.InsertData(
                table: "BookInventory",
                columns: new[] { "ID", "BookId", "CreationTime", "CreatorUserId", "LastModificationTime", "Quantity" },
                values: new object[] { new Guid("13b5ce77-b382-45b6-bff4-300bb8b93388"), new Guid("13088fc2-c0ee-487e-91a1-2b977214d0a1"), new DateTime(2022, 9, 4, 20, 51, 51, 624, DateTimeKind.Local).AddTicks(8702), "473c9df0-2634-4506-ad6c-9b98336082f2", null, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookGenresId",
                table: "Book",
                column: "BookGenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_ISBN",
                table: "Book",
                column: "ISBN",
                unique: true,
                filter: "[ISBN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookHistories_BookId",
                table: "BookHistories",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookHistories_LMUserId",
                table: "BookHistories",
                column: "LMUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInventory_BookId",
                table: "BookInventory",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookHistories");

            migrationBuilder.DropTable(
                name: "BookInventory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "BookGenres");
        }
    }
}
