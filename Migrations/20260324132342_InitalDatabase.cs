using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitalDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    HouseNumber = table.Column<int>(type: "int", nullable: true),
                    Street = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ward = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    District = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Province = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IngredientUnit = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CostPerUnit = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductionTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Utility",
                columns: table => new
                {
                    UtilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utility", x => x.UtilityID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalReviews = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    SeatingCapacity = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreID);
                    table.ForeignKey(
                        name: "FK_Store_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SupplierName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierID);
                    table.ForeignKey(
                        name: "FK_Supplier_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Receipe",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QtyBeforeProcess = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QtyAfterProcess = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipe", x => new { x.IngredientID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_Receipe_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipe_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StoreUtility",
                columns: table => new
                {
                    UtilityID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Quantity = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUtility", x => new { x.StoreID, x.UtilityID });
                    table.ForeignKey(
                        name: "FK_StoreUtility_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreUtility_Utility_UtilityID",
                        column: x => x.UtilityID,
                        principalTable: "Utility",
                        principalColumn: "UtilityID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    BasicSalary = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseID);
                    table.ForeignKey(
                        name: "FK_Warehouse_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    BillID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PaymentMethods = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_Bill_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bill_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GoodsReceipt",
                columns: table => new
                {
                    GoodsReceiptID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateReceive = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipt", x => x.GoodsReceiptID);
                    table.ForeignKey(
                        name: "FK_GoodsReceipt_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceipt_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceipt_User_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    POID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SentAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.POID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_User_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TimeIn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ChechIn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftID);
                    table.ForeignKey(
                        name: "FK_Shift_User_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_Ticket_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AddressID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => new { x.UserID, x.AddressID });
                    table.ForeignKey(
                        name: "FK_UserAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAddress_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StockAudit",
                columns: table => new
                {
                    StockAuditID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreateBy = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ApprovedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAudit", x => x.StockAuditID);
                    table.ForeignKey(
                        name: "FK_StockAudit_User_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_StockAudit_User_CreateBy",
                        column: x => x.CreateBy,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAudit_Warehouse_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillDetail",
                columns: table => new
                {
                    BillID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    InlineTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetail", x => new { x.BillID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_BillDetail_Bill_BillID",
                        column: x => x.BillID,
                        principalTable: "Bill",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReceiptDetail",
                columns: table => new
                {
                    GoodsReceiptID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetail", x => new { x.GoodsReceiptID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_GoodsReceipt_GoodsReceiptID",
                        column: x => x.GoodsReceiptID,
                        principalTable: "GoodsReceipt",
                        principalColumn: "GoodsReceiptID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "POApproval",
                columns: table => new
                {
                    POID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LastUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POApproval", x => new { x.POID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_POApproval_PurchaseOrder_POID",
                        column: x => x.POID,
                        principalTable: "PurchaseOrder",
                        principalColumn: "POID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POApproval_User_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PODetail",
                columns: table => new
                {
                    POID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "float", nullable: false),
                    UnitPriceExpected = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PODetail", x => new { x.POID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK_PODetail_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PODetail_PurchaseOrder_POID",
                        column: x => x.POID,
                        principalTable: "PurchaseOrder",
                        principalColumn: "POID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillModify",
                columns: table => new
                {
                    BillModifyID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BillID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    BillDetailBillID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BillDetailProductID = table.Column<int>(type: "int", nullable: false),
                    ModifyType = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillModify", x => x.BillModifyID);
                    table.ForeignKey(
                        name: "FK_BillModify_BillDetail_BillDetailBillID_BillDetailProductID",
                        columns: x => new { x.BillDetailBillID, x.BillDetailProductID },
                        principalTable: "BillDetail",
                        principalColumns: new[] { "BillID", "ProductID" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventoryBatch",
                columns: table => new
                {
                    BatchID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    ImportDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Exp = table.Column<DateOnly>(type: "date", nullable: false),
                    Mfd = table.Column<DateOnly>(type: "date", nullable: false),
                    QuantityOriginal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitCost = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BatchCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    GoodsReceiptID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReceiptDetailGoodsReceiptID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReceiptDetailIngredientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryBatch", x => x.BatchID);
                    table.ForeignKey(
                        name: "FK_InventoryBatch_GoodsReceipt_GoodsReceiptID",
                        column: x => x.GoodsReceiptID,
                        principalTable: "GoodsReceipt",
                        principalColumn: "GoodsReceiptID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryBatch_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryBatch_ReceiptDetail_ReceiptDetailGoodsReceiptID_Rec~",
                        columns: x => new { x.ReceiptDetailGoodsReceiptID, x.ReceiptDetailIngredientID },
                        principalTable: "ReceiptDetail",
                        principalColumns: new[] { "GoodsReceiptID", "IngredientID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryBatch_Warehouse_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StockAuditDetail",
                columns: table => new
                {
                    StockAuditID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BatchID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SysQty = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CounterQty = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Difference = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IngredientID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAuditDetail", x => new { x.StockAuditID, x.BatchID });
                    table.ForeignKey(
                        name: "FK_StockAuditDetail_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK_StockAuditDetail_InventoryBatch_BatchID",
                        column: x => x.BatchID,
                        principalTable: "InventoryBatch",
                        principalColumn: "BatchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAuditDetail_StockAudit_StockAuditID",
                        column: x => x.StockAuditID,
                        principalTable: "StockAudit",
                        principalColumn: "StockAuditID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StockMovement",
                columns: table => new
                {
                    StockMovementID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    BatchID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    EmployeeID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    QtyChange = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MovementType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReferenceType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReferenceID = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovement", x => x.StockMovementID);
                    table.ForeignKey(
                        name: "FK_StockMovement_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovement_InventoryBatch_BatchID",
                        column: x => x.BatchID,
                        principalTable: "InventoryBatch",
                        principalColumn: "BatchID");
                    table.ForeignKey(
                        name: "FK_StockMovement_User_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovement_Warehouse_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_StoreID",
                table: "Bill",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_UserID",
                table: "Bill",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_ProductID",
                table: "BillDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_BillModify_BillDetailBillID_BillDetailProductID",
                table: "BillModify",
                columns: new[] { "BillDetailBillID", "BillDetailProductID" });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipt_EmployeeID",
                table: "GoodsReceipt",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipt_StoreID",
                table: "GoodsReceipt",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipt_SupplierID",
                table: "GoodsReceipt",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatch_GoodsReceiptID",
                table: "InventoryBatch",
                column: "GoodsReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatch_IngredientID",
                table: "InventoryBatch",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatch_ReceiptDetailGoodsReceiptID_ReceiptDetailIngr~",
                table: "InventoryBatch",
                columns: new[] { "ReceiptDetailGoodsReceiptID", "ReceiptDetailIngredientID" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBatch_WarehouseID",
                table: "InventoryBatch",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_POApproval_EmployeeID",
                table: "POApproval",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_PODetail_IngredientID",
                table: "PODetail",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_EmployeeID",
                table: "PurchaseOrder",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_StoreID",
                table: "PurchaseOrder",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_SupplierID",
                table: "PurchaseOrder",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipe_ProductID",
                table: "Receipe",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_IngredientID",
                table: "ReceiptDetail",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_EmployeeID",
                table: "Shift",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_StockAudit_ApprovedBy",
                table: "StockAudit",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StockAudit_CreateBy",
                table: "StockAudit",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_StockAudit_WarehouseID",
                table: "StockAudit",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_StockAuditDetail_BatchID",
                table: "StockAuditDetail",
                column: "BatchID");

            migrationBuilder.CreateIndex(
                name: "IX_StockAuditDetail_IngredientID",
                table: "StockAuditDetail",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_BatchID",
                table: "StockMovement",
                column: "BatchID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_EmployeeID",
                table: "StockMovement",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_IngredientID",
                table: "StockMovement",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_WarehouseID",
                table: "StockMovement",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_Store_AddressID",
                table: "Store",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreUtility_UtilityID",
                table: "StoreUtility",
                column: "UtilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_AddressID",
                table: "Supplier",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserID",
                table: "Ticket",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_StoreID",
                table: "User",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_AddressID",
                table: "UserAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_StoreID",
                table: "Warehouse",
                column: "StoreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillModify");

            migrationBuilder.DropTable(
                name: "POApproval");

            migrationBuilder.DropTable(
                name: "PODetail");

            migrationBuilder.DropTable(
                name: "Receipe");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "StockAuditDetail");

            migrationBuilder.DropTable(
                name: "StockMovement");

            migrationBuilder.DropTable(
                name: "StoreUtility");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropTable(
                name: "BillDetail");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "StockAudit");

            migrationBuilder.DropTable(
                name: "InventoryBatch");

            migrationBuilder.DropTable(
                name: "Utility");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ReceiptDetail");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "GoodsReceipt");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
