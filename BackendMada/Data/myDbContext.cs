using System;
using System.Collections.Generic;
using BackendMada.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BackendMada.Data;

public partial class myDbContext : DbContext
{
    public myDbContext()
    {
        
    }

    public myDbContext(DbContextOptions<myDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> categoria { get; set; }

    public virtual DbSet<Cliente> clientes { get; set; }

    public virtual DbSet<Detalle_venta> datalle_venta { get; set; }

    public virtual DbSet<Producto> productos { get; set; }

    public virtual DbSet<Proveedor> proveedores { get; set; }

    public virtual DbSet<Venta> venta { get; set; }

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

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.id_categoria).HasName("PRIMARY");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.id_cliente).HasName("PRIMARY");
        });

        modelBuilder.Entity<Detalle_venta>(entity =>
        {
            entity.HasKey(e => e.id_detalle).HasName("PRIMARY");

            entity.HasOne(d => d.id_productoNavigation).WithMany(p => p.datalle_venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_producto");

            entity.HasOne(d => d.id_ventaNavigation).WithMany(p => p.datalle_venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_venta");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.id_producto).HasName("PRIMARY");

            entity.HasOne(d => d.id_categoriaNavigation).WithMany(p => p.productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_categoria");

            entity.HasOne(d => d.id_proveedorNavigation).WithMany(p => p.productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_proveedor");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.id_proveedor).HasName("PRIMARY");
        });

        modelBuilder.Entity<Venta>(entity =>
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
