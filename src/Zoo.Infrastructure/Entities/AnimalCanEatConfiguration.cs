﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace Zoo.Infrastructure.Entities
{
    public class AnimalCanEatConfiguration : IEntityTypeConfiguration<AnimalCanEat>
    {
        public void Configure(EntityTypeBuilder<AnimalCanEat> entity)
        {
            entity.HasKey(e => new { e.FoodId, e.FamilyId });

            entity.HasOne(d => d.Family)
                .WithMany(p => p.AnimalCanEats)
                .HasForeignKey(d => d.FamilyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalCanEats_Family");

            entity.HasOne(d => d.Food)
                .WithMany(p => p.AnimalCanEats)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalCanEats_Food");
        }
    }
}
