﻿// <auto-generated />
using System;
using CostControl.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CostControl.Data.Migrations
{
    [DbContext(typeof(CostControlDbContext))]
    [Migration("20190921011627_IsAdded")]
    partial class IsAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Buffet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("SaleCostPointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("SaleCostPointId");

                    b.ToTable("Buffet","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.ConsumptionUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

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
                        .HasMaxLength(10);

                    b.Property<long>("CostPointGroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("CostPointGroupId", "Name")
                        .IsUnique();

                    b.ToTable("CostPoint","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.CostPointGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

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
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

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

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(28,2)");

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
                        .HasColumnType("NVARCHAR(500)")
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<DateTime>("DraftDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<long>("InventoryId");

                    b.Property<DateTime>("RegisteredDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("RegisteredUserId");

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

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(28,2)");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<long>("DraftId");

                    b.Property<long>("IngredientId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("DraftId", "IngredientId")
                        .IsUnique();

                    b.ToTable("DraftItem","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Food", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(10);

                    b.Property<string>("EnglishName")
                        .HasMaxLength(250);

                    b.Property<byte>("FoodType");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<long?>("SaleCostPointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("SaleCostPointId");

                    b.HasIndex("Name", "Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

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

                    b.Property<long>("UserID");

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
                        .HasMaxLength(10);

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(500)")
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<string>("EnglishName")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.Property<decimal>("UsefullRatio")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("70");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

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
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<DateTime>("IntakeFromDate");

                    b.Property<DateTime>("IntakeToDate");

                    b.Property<bool>("IsConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("RegisteredDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("RegisteredUserId");

                    b.Property<long>("SaleCostPointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("RegisteredUserId");

                    b.HasIndex("SaleCostPointId", "IntakeFromDate", "IntakeToDate")
                        .IsUnique();

                    b.ToTable("IntakeRemittance","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IntakeRemittanceItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(28,2)");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<string>("Descripton")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(1000)")
                        .HasMaxLength(1000)
                        .IsUnicode(true);

                    b.Property<long>("IngredientId");

                    b.Property<long>("IntakeRemittanceId");

                    b.Property<bool>("IsAddedManualy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("IntakeRemittanceId", "IngredientId")
                        .IsUnique();

                    b.ToTable("IntakeRemittanceItem","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IntakeRemittanceItemLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(28,2)");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<string>("Descripton")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(1000)")
                        .HasMaxLength(1000)
                        .IsUnicode(true);

                    b.Property<long>("IngredientId");

                    b.Property<long>("IntakeRemittanceItemId");

                    b.Property<bool>("IsAddedManualy");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("ConsumptionUnitId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("IntakeRemittanceItemId");

                    b.ToTable("IntakeRemittanceItemLog","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Inventory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(10);

                    b.Property<bool>("IsWasted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

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
                        .HasMaxLength(10);

                    b.Property<decimal>("CostRatio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(28,2)")
                        .HasDefaultValueSql("0");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<DateTime>("FromDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<long>("SaleCostPointId");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.Property<DateTime?>("ToDate");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("EnglishName")
                        .IsUnique();

                    b.HasIndex("SaleCostPointId");

                    b.HasIndex("FromDate", "ToDate")
                        .IsUnique()
                        .HasFilter("[ToDate] IS NOT NULL");

                    b.ToTable("Menu","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.MenuItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(28,2)");

                    b.Property<long>("ConsumptionUnitId");

                    b.Property<long>("FoodId");

                    b.Property<long>("IngredientId");

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

                    b.Property<DateTime>("EndDate");

                    b.Property<byte>("OverCostTypeId");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("RegisteredDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("RegisteredUserId");

                    b.Property<long>("SaleCostPointId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("OverCostTypeId");

                    b.HasIndex("RegisteredUserId");

                    b.HasIndex("SaleCostPointId");

                    b.HasIndex("StartDate", "EndDate")
                        .IsUnique();

                    b.ToTable("OverCost","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.OverCostType", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FinancialCode")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("FinancialCode")
                        .IsUnique()
                        .HasFilter("[FinancialCode] IS NOT NULL");

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

                    b.Property<long?>("ConsumptionUnitId");

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

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.SalePoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("IsHall")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("EnglishName")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SalePoint","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("IngredientUsageRate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("70");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("1");

                    b.HasKey("Id");

                    b.ToTable("Setting","dbo");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Buffet", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.SaleCostPoint", "SaleCostPoint")
                        .WithMany("Buffets")
                        .HasForeignKey("SaleCostPointId")
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

                    b.HasOne("CostControl.Entity.Models.CostControl.IncommingUser", "RegisteredUser")
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
                        .HasForeignKey("SaleCostPointId");
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.Ingredient", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.ConsumptionUnit", "ConsumptionUnit")
                        .WithMany()
                        .HasForeignKey("ConsumptionUnitId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IntakeRemittance", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.IncommingUser", "RegisteredUser")
                        .WithMany("IntakeRemittances")
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
                        .HasForeignKey("IntakeRemittanceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CostControl.Entity.Models.CostControl.IntakeRemittanceItemLog", b =>
                {
                    b.HasOne("CostControl.Entity.Models.CostControl.ConsumptionUnit", "ConsumptionUnit")
                        .WithMany()
                        .HasForeignKey("ConsumptionUnitId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.IntakeRemittanceItem", "IntakeRemittanceItem")
                        .WithMany()
                        .HasForeignKey("IntakeRemittanceItemId")
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
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany("MenuItems")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict);

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

                    b.HasOne("CostControl.Entity.Models.CostControl.IncommingUser", "RegisteredUser")
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
                    b.HasOne("CostControl.Entity.Models.CostControl.ConsumptionUnit")
                        .WithMany("RecipeItems")
                        .HasForeignKey("ConsumptionUnitId");

                    b.HasOne("CostControl.Entity.Models.CostControl.Food", "Food")
                        .WithMany("RecipeItems")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CostControl.Entity.Models.CostControl.Ingredient", "Ingredient")
                        .WithMany("RecipeItems")
                        .HasForeignKey("IngredientId")
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
#pragma warning restore 612, 618
        }
    }
}
