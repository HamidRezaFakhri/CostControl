﻿// <auto-generated />
using System;
using CostControl.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CostControl.Data.Migrations
{
    [DbContext(typeof(CostControlDbContext))]
    partial class CostControlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Buffet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("SalePointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("SalePointId");

                    b.ToTable("Buffet","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.ConsumptionUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ConsumptionUnit","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.CostPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<long>("CostPointGroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("CostPointGroupId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CostPoint","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.CostPointGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CostPointGroup","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.DataImport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ImportTime")
                        .HasColumnType("DateTime");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.ToTable("DataImport","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Depo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<long>("IngredientId");

                    b.Property<long>("SaleCostPointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("SaleCostPointId");

                    b.ToTable("Depo","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Draft", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("DepoId");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<DateTime>("DraftDate")
                        .HasColumnType("datetime");

                    b.Property<long>("InventoryId");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime");

                    b.Property<long>("RegisteredUserId");

                    b.Property<long>("SaleCostPointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("DepoId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("RegisteredUserId");

                    b.HasIndex("SaleCostPointId");

                    b.ToTable("Draft","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.DraftItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<long>("DraftId");

                    b.Property<long>("IngredientId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

                    b.HasIndex("DraftId");

                    b.HasIndex("IngredientId");

                    b.ToTable("DraftItem","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Food", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<decimal>("Price");

                    b.Property<long>("SaleCostPointId");

                    b.Property<byte>("ServeType");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SaleCostPointId");

                    b.ToTable("Food","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IncommingUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OperatorCode");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.Property<int>("UserID");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("IncommingUser","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Ingredient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<string>("Description")
                        .HasColumnType("NVarChar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<decimal>("Price");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.Property<byte>("Type");

                    b.Property<decimal>("UsefullRatio")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("70");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("Ingredient","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IntakeRemittance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<DateTime>("IntakeDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime");

                    b.Property<long>("RegisteredUserId");

                    b.Property<long>("SaleCostPointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("RegisteredUserId");

                    b.HasIndex("SaleCostPointId");

                    b.ToTable("IntakeRemittance","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IntakeRemittanceItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<long>("IngredientId");

                    b.Property<long>("IntakeRemittanceID");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("IntakeRemittanceID");

                    b.ToTable("IntakeRemittanceItem","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Inventory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<bool>("IsWasted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Inventory","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Menu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<decimal>("CostRatio");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<decimal>("Price");

                    b.Property<long>("SaleCostPointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("SaleCostPointId");

                    b.ToTable("Menu","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.MenuItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<long?>("FoodId");

                    b.Property<long?>("IngredientId");

                    b.Property<long>("MenuId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

                    b.HasIndex("FoodId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItem","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.OverCost", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<byte>("OverCostTypeId");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime");

                    b.Property<long>("RegisteredUserId");

                    b.Property<long>("SaleCostPointId");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("OverCostTypeId");

                    b.HasIndex("RegisteredUserId");

                    b.HasIndex("SaleCostPointId");

                    b.ToTable("OverCost","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.OverCostType", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FinancialCode")
                        .HasMaxLength(25);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("OverCostType","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Recipe", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(28, 2)");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<decimal>("ConvertionRate")
                        .HasColumnType("numeric(28, 2)");

                    b.Property<long>("FoodId");

                    b.Property<long>("IngredientId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("FoodId", "IngredientId")
                        .IsUnique();

                    b.ToTable("Recipe","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Sale", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<long>("SaleCostPointId");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("SaleCostPointId");

                    b.ToTable("Sale","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.SaleCostPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CostPointId");

                    b.Property<long>("SalePointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("CostPointId");

                    b.HasIndex("SalePointId", "CostPointId")
                        .IsUnique();

                    b.ToTable("SaleCostPoint","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.SaleItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<long>("FoodId");

                    b.Property<long>("IngredientId");

                    b.Property<decimal>("Price");

                    b.Property<long>("SaleId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("SaleId");

                    b.ToTable("SaleItem","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.SalePoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("IsHall");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SalePoint","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("IngredientUsageRate");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.ToTable("Setting","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.Security.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(25);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("CostControl.Entity.Models.Security.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("RoleId");

                    b.Property<int>("State");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Buffet", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.SalePoint", "SalePoint")
                        .WithMany("Buffets")
                        .HasForeignKey("SalePointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.CostPoint", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.CostPointGroup", "CostPointGroup")
                        .WithMany("CostPoints")
                        .HasForeignKey("CostPointGroupId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Depo", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.ConsumptionUnit", "ConsumptionUnit")
                        .WithMany()
                        .HasForeignKey("ConsumptionUnitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.SaleCostPoint", "SaleCostPoint")
                        .WithMany("Depos")
                        .HasForeignKey("SaleCostPointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Draft", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.Depo", "Depo")
                        .WithMany("Drafts")
                        .HasForeignKey("DepoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Inventory", "Inventory")
                        .WithMany("Drafts")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.Security.User", "RegisteredUser")
                        .WithMany("Drafts")
                        .HasForeignKey("RegisteredUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.SaleCostPoint", "SaleCostPoint")
                        .WithMany("Drafts")
                        .HasForeignKey("SaleCostPointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.DraftItem", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.ConsumptionUnit", "ConsumptionUnit")
                        .WithMany("DraftItems")
                        .HasForeignKey("ConsumptionUnitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Draft", "Draft")
                        .WithMany("DraftItems")
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Food", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.SaleCostPoint", "SaleCostPoint")
                        .WithMany("Foods")
                        .HasForeignKey("SaleCostPointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IntakeRemittance", b =>
                {
                    b.HasOne("CostControl.Entity.Models.Security.User", "RegisteredUser")
                        .WithMany()
                        .HasForeignKey("RegisteredUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.SaleCostPoint", "SaleCostPoint")
                        .WithMany("IntakeRemittances")
                        .HasForeignKey("SaleCostPointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IntakeRemittanceItem", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.ConsumptionUnit", "ConsumptionUnit")
                        .WithMany()
                        .HasForeignKey("ConsumptionUnitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.IntakeRemittance", "IntakeRemittance")
                        .WithMany("IntakeRemittanceItems")
                        .HasForeignKey("IntakeRemittanceID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Menu", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.SaleCostPoint", "SaleCostPoint")
                        .WithMany("Menus")
                        .HasForeignKey("SaleCostPointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.MenuItem", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.ConsumptionUnit", "ConsumptionUnit")
                        .WithMany("MenuItems")
                        .HasForeignKey("ConsumptionUnitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Food", "Food")
                        .WithMany("MenuItems")
                        .HasForeignKey("FoodId");

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany("MenuItems")
                        .HasForeignKey("IngredientId");

                    b.HasOne("CostControl.Entity.Models.CostControl.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.OverCost", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.OverCostType", "OverCostType")
                        .WithMany("OverCosts")
                        .HasForeignKey("OverCostTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.Security.User", "RegisteredUser")
                        .WithMany("OverCosts")
                        .HasForeignKey("RegisteredUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.SaleCostPoint", "SaleCostPoint")
                        .WithMany("OverCosts")
                        .HasForeignKey("SaleCostPointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Recipe", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.ConsumptionUnit", "ConsumptionUnit")
                        .WithMany("RecipeItems")
                        .HasForeignKey("ConsumptionUnitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Food", "Food")
                        .WithMany("RecipeItems")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany("RecipeItems")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Sale", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.SaleCostPoint", "SaleCostPoint")
                        .WithMany("Sales")
                        .HasForeignKey("SaleCostPointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.SaleCostPoint", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.CostPoint", "CostPoint")
                        .WithMany("SaleCostPoints")
                        .HasForeignKey("CostPointId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.SalePoint", "SalePoint")
                        .WithMany("SaleCostPoints")
                        .HasForeignKey("SalePointId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.SaleItem", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.Food", "Food")
                        .WithMany("SaleItems")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany("SaleItems")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Sale", "Sale")
                        .WithMany("SaleItems")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.Security.User", b =>
                {
                    b.HasOne("CostControl.Entity.Models.Security.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
