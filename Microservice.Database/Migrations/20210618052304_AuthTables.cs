using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microservice.Database.Migrations
{
    public partial class AuthTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PasswordResetGuid = table.Column<string>(nullable: true),
                    PasswordResetExpiry = table.Column<DateTime>(nullable: true),
                    IncorrectLoginAttempts = table.Column<int>(nullable: false),
                    LockedOutUntilUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifiedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    SessionIdentifier = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    ExpiresAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
