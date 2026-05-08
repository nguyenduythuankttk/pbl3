using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class verify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEmailVerified",
                table: "User",
                newName: "IsVerified");

            migrationBuilder.AddColumn<string>(
                name: "EmailVerified",
                table: "User",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PasswordEmail",
                table: "User",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordEmailExp",
                table: "User",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedExp",
                table: "User",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerified",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordEmail",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordEmailExp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "VerifiedExp",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "User",
                newName: "IsEmailVerified");
        }
    }
}
