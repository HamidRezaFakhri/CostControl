using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostControl.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ConsumptionUnit",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostPointGroup",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostPointGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataImport",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    ImportTime = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataImport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncommingUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    UserID = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    OperatorCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncommingUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    IsWasted = table.Column<bool>(nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OverCostType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    FinancialCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverCostType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalePoint",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 250, nullable: false),
                    IsHall = table.Column<bool>(nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalePoint", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    IngredientUsageRate = table.Column<decimal>(nullable: false, defaultValueSql: "70")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 250, nullable: true),
                    ConsumptionUnitId = table.Column<long>(nullable: false),
                    UsefullRatio = table.Column<decimal>(nullable: false, defaultValueSql: "70"),
                    Description = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_ConsumptionUnit_ConsumptionUnitId",
                        column: x => x.ConsumptionUnitId,
                        principalSchema: "dbo",
                        principalTable: "ConsumptionUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostPoint",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    CostPointGroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostPoint_CostPointGroup_CostPointGroupId",
                        column: x => x.CostPointGroupId,
                        principalSchema: "dbo",
                        principalTable: "CostPointGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleCostPoint",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SalePointId = table.Column<long>(nullable: false),
                    CostPointId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleCostPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleCostPoint_CostPoint_CostPointId",
                        column: x => x.CostPointId,
                        principalSchema: "dbo",
                        principalTable: "CostPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleCostPoint_SalePoint_SalePointId",
                        column: x => x.SalePointId,
                        principalSchema: "dbo",
                        principalTable: "SalePoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buffet",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    SalePointId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buffet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buffet_SaleCostPoint_SaleCostPointId",
                        column: x => x.SaleCostPointId,
                        principalSchema: "dbo",
                        principalTable: "SaleCostPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buffet_SalePoint_SalePointId",
                        column: x => x.SalePointId,
                        principalSchema: "dbo",
                        principalTable: "SalePoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Depo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    ConsumptionUnitId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(28,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Depo_ConsumptionUnit_ConsumptionUnitId",
                        column: x => x.ConsumptionUnitId,
                        principalSchema: "dbo",
                        principalTable: "ConsumptionUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Depo_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "dbo",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Depo_SaleCostPoint_SaleCostPointId",
                        column: x => x.SaleCostPointId,
                        principalSchema: "dbo",
                        principalTable: "SaleCostPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    SaleCostPointId = table.Column<long>(nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 250, nullable: true),
                    FoodType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_SaleCostPoint_SaleCostPointId",
                        column: x => x.SaleCostPointId,
                        principalSchema: "dbo",
                        principalTable: "SaleCostPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IntakeRemittance",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    IntakeDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    RegisteredDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    RegisteredUserId = table.Column<long>(nullable: false),
                    RegisteredUserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeRemittance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntakeRemittance_IncommingUser_RegisteredUserId1",
                        column: x => x.RegisteredUserId1,
                        principalSchema: "dbo",
                        principalTable: "IncommingUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntakeRemittance_SaleCostPoint_SaleCostPointId",
                        column: x => x.SaleCostPointId,
                        principalSchema: "dbo",
                        principalTable: "SaleCostPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(nullable: true),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 250, nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ToDate = table.Column<DateTime>(nullable: true),
                    CostRatio = table.Column<decimal>(type: "numeric(28,2)", nullable: false, defaultValueSql: "0"),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_SaleCostPoint_SaleCostPointId",
                        column: x => x.SaleCostPointId,
                        principalSchema: "dbo",
                        principalTable: "SaleCostPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OverCost",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    OverCostTypeId = table.Column<byte>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    RegisteredDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    RegisteredUserId = table.Column<long>(nullable: false),
                    RegisteredUserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverCost_OverCostType_OverCostTypeId",
                        column: x => x.OverCostTypeId,
                        principalSchema: "dbo",
                        principalTable: "OverCostType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OverCost_IncommingUser_RegisteredUserId1",
                        column: x => x.RegisteredUserId1,
                        principalSchema: "dbo",
                        principalTable: "IncommingUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OverCost_SaleCostPoint_SaleCostPointId",
                        column: x => x.SaleCostPointId,
                        principalSchema: "dbo",
                        principalTable: "SaleCostPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Draft",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    InventoryId = table.Column<long>(nullable: false),
                    DepoId = table.Column<long>(nullable: false),
                    DraftDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    RegisteredDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    RegisteredUserId = table.Column<long>(nullable: false),
                    RegisteredUserId1 = table.Column<int>(nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draft", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Draft_Depo_DepoId",
                        column: x => x.DepoId,
                        principalSchema: "dbo",
                        principalTable: "Depo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Draft_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalSchema: "dbo",
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Draft_IncommingUser_RegisteredUserId1",
                        column: x => x.RegisteredUserId1,
                        principalSchema: "dbo",
                        principalTable: "IncommingUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Draft_SaleCostPoint_SaleCostPointId",
                        column: x => x.SaleCostPointId,
                        principalSchema: "dbo",
                        principalTable: "SaleCostPoint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    FoodId = table.Column<long>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(28, 2)", nullable: false),
                    ConsumptionUnitId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_ConsumptionUnit_ConsumptionUnitId",
                        column: x => x.ConsumptionUnitId,
                        principalSchema: "dbo",
                        principalTable: "ConsumptionUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipe_Food_FoodId",
                        column: x => x.FoodId,
                        principalSchema: "dbo",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipe_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "dbo",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IntakeRemittanceItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    IntakeRemittanceID = table.Column<long>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(28,2)", nullable: false),
                    ConsumptionUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeRemittanceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntakeRemittanceItem_ConsumptionUnit_ConsumptionUnitId",
                        column: x => x.ConsumptionUnitId,
                        principalSchema: "dbo",
                        principalTable: "ConsumptionUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntakeRemittanceItem_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "dbo",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntakeRemittanceItem_IntakeRemittance_IntakeRemittanceID",
                        column: x => x.IntakeRemittanceID,
                        principalSchema: "dbo",
                        principalTable: "IntakeRemittance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    MenuId = table.Column<long>(nullable: false),
                    FoodId = table.Column<long>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(28,2)", nullable: false),
                    ConsumptionUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItem_ConsumptionUnit_ConsumptionUnitId",
                        column: x => x.ConsumptionUnitId,
                        principalSchema: "dbo",
                        principalTable: "ConsumptionUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItem_Food_FoodId",
                        column: x => x.FoodId,
                        principalSchema: "dbo",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItem_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "dbo",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItem_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "dbo",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DraftItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    DraftId = table.Column<long>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    ConsumptionUnitId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(28,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftItem_ConsumptionUnit_ConsumptionUnitId",
                        column: x => x.ConsumptionUnitId,
                        principalSchema: "dbo",
                        principalTable: "ConsumptionUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftItem_Draft_DraftId",
                        column: x => x.DraftId,
                        principalSchema: "dbo",
                        principalTable: "Draft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DraftItem_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "dbo",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buffet_SaleCostPointId",
                schema: "dbo",
                table: "Buffet",
                column: "SaleCostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Buffet_SalePointId",
                schema: "dbo",
                table: "Buffet",
                column: "SalePointId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionUnit_Code",
                schema: "dbo",
                table: "ConsumptionUnit",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionUnit_Name",
                schema: "dbo",
                table: "ConsumptionUnit",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostPoint_Code",
                schema: "dbo",
                table: "CostPoint",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CostPoint_CostPointGroupId_Name",
                schema: "dbo",
                table: "CostPoint",
                columns: new[] { "CostPointGroupId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostPointGroup_Code",
                schema: "dbo",
                table: "CostPointGroup",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CostPointGroup_Name",
                schema: "dbo",
                table: "CostPointGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Depo_ConsumptionUnitId",
                schema: "dbo",
                table: "Depo",
                column: "ConsumptionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Depo_IngredientId",
                schema: "dbo",
                table: "Depo",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Depo_SaleCostPointId",
                schema: "dbo",
                table: "Depo",
                column: "SaleCostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Draft_DepoId",
                schema: "dbo",
                table: "Draft",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_Draft_InventoryId",
                schema: "dbo",
                table: "Draft",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Draft_RegisteredUserId1",
                schema: "dbo",
                table: "Draft",
                column: "RegisteredUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Draft_SaleCostPointId",
                schema: "dbo",
                table: "Draft",
                column: "SaleCostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftItem_ConsumptionUnitId",
                schema: "dbo",
                table: "DraftItem",
                column: "ConsumptionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftItem_IngredientId",
                schema: "dbo",
                table: "DraftItem",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftItem_DraftId_IngredientId",
                schema: "dbo",
                table: "DraftItem",
                columns: new[] { "DraftId", "IngredientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_SaleCostPointId",
                schema: "dbo",
                table: "Food",
                column: "SaleCostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_Name_Code",
                schema: "dbo",
                table: "Food",
                columns: new[] { "Name", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_ConsumptionUnitId",
                schema: "dbo",
                table: "Ingredient",
                column: "ConsumptionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Name_Code",
                schema: "dbo",
                table: "Ingredient",
                columns: new[] { "Name", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeRemittance_RegisteredUserId1",
                schema: "dbo",
                table: "IntakeRemittance",
                column: "RegisteredUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeRemittance_SaleCostPointId",
                schema: "dbo",
                table: "IntakeRemittance",
                column: "SaleCostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeRemittanceItem_ConsumptionUnitId",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                column: "ConsumptionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeRemittanceItem_IngredientId",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeRemittanceItem_IntakeRemittanceID",
                schema: "dbo",
                table: "IntakeRemittanceItem",
                column: "IntakeRemittanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Code",
                schema: "dbo",
                table: "Inventory",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Name",
                schema: "dbo",
                table: "Inventory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Code",
                schema: "dbo",
                table: "Menu",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_EnglishName",
                schema: "dbo",
                table: "Menu",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_SaleCostPointId",
                schema: "dbo",
                table: "Menu",
                column: "SaleCostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_FromDate_ToDate",
                schema: "dbo",
                table: "Menu",
                columns: new[] { "FromDate", "ToDate" },
                unique: true,
                filter: "[ToDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_ConsumptionUnitId",
                schema: "dbo",
                table: "MenuItem",
                column: "ConsumptionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_FoodId",
                schema: "dbo",
                table: "MenuItem",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_IngredientId",
                schema: "dbo",
                table: "MenuItem",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuId",
                schema: "dbo",
                table: "MenuItem",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OverCost_OverCostTypeId",
                schema: "dbo",
                table: "OverCost",
                column: "OverCostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OverCost_RegisteredUserId1",
                schema: "dbo",
                table: "OverCost",
                column: "RegisteredUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_OverCost_SaleCostPointId",
                schema: "dbo",
                table: "OverCost",
                column: "SaleCostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_OverCost_StartDate_EndDate",
                schema: "dbo",
                table: "OverCost",
                columns: new[] { "StartDate", "EndDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OverCostType_FinancialCode",
                schema: "dbo",
                table: "OverCostType",
                column: "FinancialCode",
                unique: true,
                filter: "[FinancialCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OverCostType_Name",
                schema: "dbo",
                table: "OverCostType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_ConsumptionUnitId",
                schema: "dbo",
                table: "Recipe",
                column: "ConsumptionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_IngredientId",
                schema: "dbo",
                table: "Recipe",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_FoodId_IngredientId",
                schema: "dbo",
                table: "Recipe",
                columns: new[] { "FoodId", "IngredientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleCostPoint_CostPointId",
                schema: "dbo",
                table: "SaleCostPoint",
                column: "CostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleCostPoint_SalePointId_CostPointId",
                schema: "dbo",
                table: "SaleCostPoint",
                columns: new[] { "SalePointId", "CostPointId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalePoint_Code",
                schema: "dbo",
                table: "SalePoint",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalePoint_EnglishName",
                schema: "dbo",
                table: "SalePoint",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalePoint_Name",
                schema: "dbo",
                table: "SalePoint",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buffet",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DataImport",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DraftItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IntakeRemittanceItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MenuItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OverCost",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Recipe",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Draft",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IntakeRemittance",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OverCostType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Food",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Depo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Inventory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "IncommingUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Ingredient",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SaleCostPoint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ConsumptionUnit",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CostPoint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SalePoint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CostPointGroup",
                schema: "dbo");
        }
    }
}
