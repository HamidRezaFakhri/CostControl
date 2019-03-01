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
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionUnit",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true)
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
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true)
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
                    ImportTime = table.Column<DateTime>(type: "DateTime", nullable: false)
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
                    UserID = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    OperatorCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncommingUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 250, nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    UsefullRatio = table.Column<decimal>(nullable: false, defaultValueSql: "70"),
                    Description = table.Column<string>(type: "NVarChar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    IsWasted = table.Column<bool>(nullable: false)
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
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    FinancialCode = table.Column<string>(maxLength: 25, nullable: true)
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
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 250, nullable: false),
                    IsHall = table.Column<bool>(nullable: false)
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
                    IngredientUsageRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
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
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
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
                name: "Buffet",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SalePointId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buffet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buffet_SalePoint_SalePointId",
                        column: x => x.SalePointId,
                        principalSchema: "dbo",
                        principalTable: "SalePoint",
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
                    Amount = table.Column<decimal>(nullable: false)
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
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 250, nullable: false),
                    ServeType = table.Column<byte>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
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
                    IntakeDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RegisteredUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeRemittance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntakeRemittance_User_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "User",
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
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 250, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CostRatio = table.Column<decimal>(nullable: false),
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
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RegisteredUserId = table.Column<long>(nullable: false)
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
                        name: "FK_OverCost_User_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "User",
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
                name: "Sale",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SaleCostPointId = table.Column<long>(nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Code = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_SaleCostPoint_SaleCostPointId",
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
                    DraftDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RegisteredUserId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
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
                        name: "FK_Draft_User_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "User",
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
                    ConsumptionUnitId = table.Column<long>(nullable: false),
                    ConvertionRate = table.Column<decimal>(type: "numeric(28, 2)", nullable: false)
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
                    Amount = table.Column<decimal>(nullable: false),
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
                    Amount = table.Column<decimal>(nullable: false),
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
                name: "SaleItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<int>(nullable: false, defaultValueSql: "1"),
                    SaleId = table.Column<long>(nullable: false),
                    FoodId = table.Column<long>(nullable: false),
                    IngredientId = table.Column<long>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItem_Food_FoodId",
                        column: x => x.FoodId,
                        principalSchema: "dbo",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleItem_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "dbo",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleItem_Sale_SaleId",
                        column: x => x.SaleId,
                        principalSchema: "dbo",
                        principalTable: "Sale",
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
                    Amount = table.Column<decimal>(nullable: false)
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
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Buffet_SalePointId",
                schema: "dbo",
                table: "Buffet",
                column: "SalePointId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionUnit_Name",
                schema: "dbo",
                table: "ConsumptionUnit",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostPoint_CostPointGroupId",
                schema: "dbo",
                table: "CostPoint",
                column: "CostPointGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CostPoint_Name",
                schema: "dbo",
                table: "CostPoint",
                column: "Name",
                unique: true);

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
                name: "IX_Draft_RegisteredUserId",
                schema: "dbo",
                table: "Draft",
                column: "RegisteredUserId");

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
                name: "IX_DraftItem_DraftId",
                schema: "dbo",
                table: "DraftItem",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftItem_IngredientId",
                schema: "dbo",
                table: "DraftItem",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_Name",
                schema: "dbo",
                table: "Food",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_SaleCostPointId",
                schema: "dbo",
                table: "Food",
                column: "SaleCostPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Name_Code",
                schema: "dbo",
                table: "Ingredient",
                columns: new[] { "Name", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeRemittance_RegisteredUserId",
                schema: "dbo",
                table: "IntakeRemittance",
                column: "RegisteredUserId");

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
                name: "IX_Inventory_Name",
                schema: "dbo",
                table: "Inventory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_SaleCostPointId",
                schema: "dbo",
                table: "Menu",
                column: "SaleCostPointId");

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
                name: "IX_OverCost_RegisteredUserId",
                schema: "dbo",
                table: "OverCost",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OverCost_SaleCostPointId",
                schema: "dbo",
                table: "OverCost",
                column: "SaleCostPointId");

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
                name: "IX_Sale_SaleCostPointId",
                schema: "dbo",
                table: "Sale",
                column: "SaleCostPointId");

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
                name: "IX_SaleItem_FoodId",
                schema: "dbo",
                table: "SaleItem",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_IngredientId",
                schema: "dbo",
                table: "SaleItem",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_SaleId",
                schema: "dbo",
                table: "SaleItem",
                column: "SaleId");

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
                name: "IncommingUser",
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
                name: "SaleItem",
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
                name: "Sale",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Depo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Inventory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ConsumptionUnit",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Ingredient",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SaleCostPoint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Role");

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
