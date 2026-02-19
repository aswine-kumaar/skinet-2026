using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Core.Entities.Product>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.Product> builder)
    {
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
    }
}
