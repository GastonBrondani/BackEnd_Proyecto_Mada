using System;
using System.Collections.Generic;
using BackendMada.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BackendMada.Data;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<categoria> categoria { get; set; }

    public virtual DbSet<cliente> clientes { get; set; }

    public virtual DbSet<datalle_venta> datalle_venta { get; set; }

    public virtual DbSet<producto> productos { get; set; }

    public virtual DbSet<proveedor> proveedores { get; set; }

    public virtual DbSet<venta> venta { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
             optionsBuilder.UseMySql("server=localhost;database=madadb;user=root;password=parlante3", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));
        }
    }*/


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<categoria>(entity =>
        {
            entity.HasKey(e => e.id_categoria).HasName("PRIMARY");
        });

        modelBuilder.Entity<cliente>(entity =>
        {
            entity.HasKey(e => e.id_cliente).HasName("PRIMARY");
        });

        modelBuilder.Entity<datalle_venta>(entity =>
        {
            entity.HasKey(e => e.id_detalle).HasName("PRIMARY");

            entity.HasOne(d => d.id_productoNavigation).WithMany(p => p.datalle_venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_producto");

            entity.HasOne(d => d.id_ventaNavigation).WithMany(p => p.datalle_venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_venta");
        });

        modelBuilder.Entity<producto>(entity =>
        {
            entity.HasKey(e => e.id_producto).HasName("PRIMARY");

            entity.HasOne(d => d.id_categoriaNavigation).WithMany(p => p.productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_categoria");

            entity.HasOne(d => d.id_proveedorNavigation).WithMany(p => p.productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_proveedor");
        });

        modelBuilder.Entity<proveedor>(entity =>
        {
            entity.HasKey(e => e.id_proveedor).HasName("PRIMARY");
        });

        modelBuilder.Entity<venta>(entity =>
        {
            entity.HasKey(e => e.id_venta).HasName("PRIMARY");

            entity.Property(e => e.fecha_venta).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.id_clienteNavigation).WithMany(p => p.venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
