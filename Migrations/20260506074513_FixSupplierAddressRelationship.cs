using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class FixSupplierAddressRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Address_AddressID",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_AddressID",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Supplier");

            migrationBuilder.CreateIndex(
                name: "IX_Address_SupplierID",
                table: "Address",
                column: "SupplierID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Supplier_SupplierID",
                table: "Address",
                column: "SupplierID",
                principalTable: "Supplier",
                principalColumn: "SupplierID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Supplier_SupplierID",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_SupplierID",
                table: "Address");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressID",
                table: "Supplier",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_AddressID",
                table: "Supplier",
                column: "AddressID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Address_AddressID",
                table: "Supplier",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
