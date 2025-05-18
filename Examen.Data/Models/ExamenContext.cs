using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Examen.API.Data.Models;

public partial class ExamenContext : DbContext
{
    public ExamenContext()
    {
    }

    public ExamenContext(DbContextOptions<ExamenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblFactura> TblFacturas { get; set; }

    public virtual DbSet<TblPersona> TblPersonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlite("Data Source=examen.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblFactura>(entity =>
        {
            entity.ToTable("tbl_factura");

            entity.Property(e => e.Id).HasColumnName("id");

            // SQLite no tiene tipo datetime nativo, EF Core lo convierte internamente
            entity.Property(e => e.Fecha)
                .HasColumnName("fecha");

            entity.Property(e => e.IdPersona).HasColumnName("id_persona");

            // SQLite no soporta decimal directamente, EF Core lo mapea a REAL (double)
            entity.Property(e => e.Monto)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblFacturas)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_factura_tbl_persona");
        });

        modelBuilder.Entity<TblPersona>(entity =>
        {
            entity.ToTable("tbl_persona");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .HasColumnName("apellidoMaterno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .HasColumnName("apellidoPaterno");
            entity.Property(e => e.Identificacion).HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Token).HasColumnName("token");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
