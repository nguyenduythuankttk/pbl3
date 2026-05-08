using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class FixStoreAddressRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Address_AddressID",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_AddressID",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Store");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StoreID",
                table: "Address",
                column: "StoreID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Store_StoreID",
                table: "Address",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "StoreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Store_StoreID",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_StoreID",
                table: "Address");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressID",
                table: "Store",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Store_AddressID",
                table: "Store",
                column: "AddressID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Address_AddressID",
                table: "Store",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
