using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "User",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "StockMovement",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Shift",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PurchaseOrder",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ProductVarient",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Product",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Ingredient",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DiningTable",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DiningTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "DeliveryInfo",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Combo",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Category",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Booking",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "StockMovement");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ProductVarient");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DiningTable");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DiningTable");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "DeliveryInfo");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Combo");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Booking");
        }
    }
}
