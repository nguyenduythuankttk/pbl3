using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class deletebehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Address_AddressID",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Store_Address_AddressID",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_AddressID",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Store_AddressID",
                table: "Store");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketID1",
                table: "TicketProduct",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProduct_TicketID1",
                table: "TicketProduct",
                column: "TicketID1");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_AddressID",
                table: "Supplier",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Store_AddressID",
                table: "Store",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StoreID",
                table: "Address",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_SupplierID",
                table: "Address",
                column: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Store_StoreID",
                table: "Address",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Supplier_SupplierID",
                table: "Address",
                column: "SupplierID",
                principalTable: "Supplier",
                principalColumn: "SupplierID");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketProduct_Ticket_TicketID1",
                table: "TicketProduct",
                column: "TicketID1",
                principalTable: "Ticket",
                principalColumn: "TicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Address_AddressID",
                table: "Supplier",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Address_AddressID",
                table: "Store",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Address_AddressID",
                table: "Store");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Address_AddressID",
                table: "Supplier");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Store_StoreID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Supplier_SupplierID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketProduct_Ticket_TicketID1",
                table: "TicketProduct");

            migrationBuilder.DropIndex(
                name: "IX_TicketProduct_TicketID1",
                table: "TicketProduct");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_AddressID",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Store_AddressID",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Address_StoreID",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_SupplierID",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "TicketID1",
                table: "TicketProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_AddressID",
                table: "Supplier",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Store_AddressID",
                table: "Store",
                column: "AddressID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Address_AddressID",
                table: "Supplier",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

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
