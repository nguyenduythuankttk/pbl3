using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class fixa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryBatch_ReceiptDetail_ReceiptDetailGoodsReceiptID_Rec~",
                table: "InventoryBatch");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiptDetailIngredientID",
                table: "InventoryBatch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiptDetailGoodsReceiptID",
                table: "InventoryBatch",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBatch_ReceiptDetail_ReceiptDetailGoodsReceiptID_Rec~",
                table: "InventoryBatch",
                columns: new[] { "ReceiptDetailGoodsReceiptID", "ReceiptDetailIngredientID" },
                principalTable: "ReceiptDetail",
                principalColumns: new[] { "GoodsReceiptID", "IngredientID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryBatch_ReceiptDetail_ReceiptDetailGoodsReceiptID_Rec~",
                table: "InventoryBatch");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiptDetailIngredientID",
                table: "InventoryBatch",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiptDetailGoodsReceiptID",
                table: "InventoryBatch",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBatch_ReceiptDetail_ReceiptDetailGoodsReceiptID_Rec~",
                table: "InventoryBatch",
                columns: new[] { "ReceiptDetailGoodsReceiptID", "ReceiptDetailIngredientID" },
                principalTable: "ReceiptDetail",
                principalColumns: new[] { "GoodsReceiptID", "IngredientID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
