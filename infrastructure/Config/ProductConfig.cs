﻿using core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Config;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.PictureUrl).IsRequired();
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.HasOne(b => b.ProductBrand)
            .WithMany()
            .HasForeignKey(p => p.ProductBrandId);
        builder.HasOne(t => t.ProductType)
            .WithMany()
            .HasForeignKey(p => p.ProductTypeId);
    }
}