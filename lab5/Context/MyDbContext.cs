using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using lab5.Entities;

namespace lab5.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<UnitsOfMeasure> UnitsOfMeasures { get; set; }
    
    private readonly string _connectionString = System.Configuration.ConfigurationManager
        .ConnectionStrings["connectionStrings"].ConnectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => e.Article, "products_article_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Article)
                .HasMaxLength(255)
                .HasColumnName("article");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity)
                .HasPrecision(10, 2)
                .HasColumnName("quantity");
            entity.Property(e => e.UnitOfMeasureId).HasColumnName("unit_of_measure_id");

            entity.HasOne(d => d.UnitOfMeasure).WithMany(p => p.Products)
                .HasForeignKey(d => d.UnitOfMeasureId)
                .HasConstraintName("products_unit_of_measure_id_fkey");
        });

        modelBuilder.Entity<UnitsOfMeasure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("units_of_measure_pkey");

            entity.ToTable("units_of_measure");

            entity.HasIndex(e => e.Name, "units_of_measure_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
