using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class CheckPending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_User_DeletedBy",
                table: "Receipt");

            migrationBuilder.DropTable(
                name: "InspectionDetail");

            migrationBuilder.DropTable(
                name: "GoodsInspection");

            migrationBuilder.DropIndex(
                name: "IX_Receipt_DeletedBy",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "DateReceive",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Receipt");

            migrationBuilder.AddColumn<decimal>(
                name: "GoodQuantity",
                table: "ReceiptDetail",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeID",
                table: "POApprovalChange",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "POID",
                table: "POApprovalChange",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "AftStatus",
                table: "POApproval",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BfrStatus",
                table: "POApproval",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "POApproval",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "POApproval",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "POApprovalID",
                table: "POApproval",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ReceiptChange",
                columns: table => new
                {
                    ReceiptChangeID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReceiptID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmployeeID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptChange", x => x.ReceiptChangeID);
                    table.ForeignKey(
                        name: "FK_ReceiptChange_Receipt_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipt",
                        principalColumn: "GoodsReceiptID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptChange_User_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_POApprovalChange_EmployeeID",
                table: "POApprovalChange",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_POApprovalChange_POID",
                table: "POApprovalChange",
                column: "POID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptChange_EmployeeID",
                table: "ReceiptChange",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptChange_ReceiptID",
                table: "ReceiptChange",
                column: "ReceiptID");

            migrationBuilder.AddForeignKey(
                name: "FK_POApprovalChange_PurchaseOrder_POID",
                table: "POApprovalChange",
                column: "POID",
                principalTable: "PurchaseOrder",
                principalColumn: "POID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_POApprovalChange_User_EmployeeID",
                table: "POApprovalChange",
                column: "EmployeeID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POApprovalChange_PurchaseOrder_POID",
                table: "POApprovalChange");

            migrationBuilder.DropForeignKey(
                name: "FK_POApprovalChange_User_EmployeeID",
                table: "POApprovalChange");

            migrationBuilder.DropTable(
                name: "ReceiptChange");

            migrationBuilder.DropIndex(
                name: "IX_POApprovalChange_EmployeeID",
                table: "POApprovalChange");

            migrationBuilder.DropIndex(
                name: "IX_POApprovalChange_POID",
                table: "POApprovalChange");

            migrationBuilder.DropColumn(
                name: "GoodQuantity",
                table: "ReceiptDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "POApprovalChange");

            migrationBuilder.DropColumn(
                name: "POID",
                table: "POApprovalChange");

            migrationBuilder.DropColumn(
                name: "AftStatus",
                table: "POApproval");

            migrationBuilder.DropColumn(
                name: "BfrStatus",
                table: "POApproval");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "POApproval");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "POApproval");

            migrationBuilder.DropColumn(
                name: "POApprovalID",
                table: "POApproval");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReceive",
                table: "Receipt",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "Receipt",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Receipt",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Receipt",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GoodsInspection",
                columns: table => new
                {
                    InspectionID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InspectedBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReceiptID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InspectedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsInspection", x => x.InspectionID);
                    table.ForeignKey(
                        name: "FK_GoodsInspection_Receipt_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipt",
                        principalColumn: "GoodsReceiptID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsInspection_User_InspectedBy",
                        column: x => x.InspectedBy,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InspectionDetail",
                columns: table => new
                {
                    InspectionID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    DamageReason = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DamagedQty = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ExpiredQty = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    GoodQty = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReceivedQty = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShortageQty = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionDetail", x => new { x.InspectionID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK_InspectionDetail_GoodsInspection_InspectionID",
                        column: x => x.InspectionID,
                        principalTable: "GoodsInspection",
                        principalColumn: "InspectionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InspectionDetail_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_DeletedBy",
                table: "Receipt",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsInspection_InspectedBy",
                table: "GoodsInspection",
                column: "InspectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsInspection_ReceiptID",
                table: "GoodsInspection",
                column: "ReceiptID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InspectionDetail_IngredientID",
                table: "InspectionDetail",
                column: "IngredientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_User_DeletedBy",
                table: "Receipt",
                column: "DeletedBy",
                principalTable: "User",
                principalColumn: "UserID");
        }
    }
}
