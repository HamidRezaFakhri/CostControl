﻿using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class RecipeConfigure : BaseEntityConfigure<Recipe, long>
    {
        public override void Configure(EntityTypeBuilder<Recipe> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            
            entityTypeBuilder
                .Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("numeric(28, 2)");

            entityTypeBuilder
                .Property(e => e.ConsumptionUnitId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.ConvertionRate)
                .IsRequired()
                .HasColumnType("numeric(28, 2)");

            entityTypeBuilder
                .HasIndex(e => new { e.FoodId, e.IngredientId })
                .IsUnique();

            entityTypeBuilder
               .ToTable("Recipe", "dbo");
        }
    }
}