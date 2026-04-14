using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiptFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_PurchaseOrder_POID",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_User_DeletedBy",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Receipt");

            migrationBuilder.AlterColumn<Guid>(
                name: "POID",
                table: "Receipt",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeletedBy",
                table: "Receipt",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReceive",
                table: "Receipt",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "Receipt",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_PurchaseOrder_POID",
                table: "Receipt",
                column: "POID",
                principalTable: "PurchaseOrder",
                principalColumn: "POID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_User_DeletedBy",
                table: "Receipt",
                column: "DeletedBy",
                principalTable: "User",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_PurchaseOrder_POID",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_User_DeletedBy",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "Receipt");

            migrationBuilder.AlterColumn<Guid>(
                name: "POID",
                table: "Receipt",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeletedBy",
                table: "Receipt",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateReceive",
                table: "Receipt",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Receipt",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_PurchaseOrder_POID",
                table: "Receipt",
                column: "POID",
                principalTable: "PurchaseOrder",
                principalColumn: "POID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_User_DeletedBy",
                table: "Receipt",
                column: "DeletedBy",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
