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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ALEJANDRO\\SQLEXPRESS;Database=Examen;Trusted_Connection=True; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblFactura>(entity =>
        {
            entity.ToTable("tbl_factura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 2)")
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
