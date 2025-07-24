using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SASF_EAPRAEZ_KRUGER.Entities;

namespace SASF_EAPRAEZ_KRUGER.Data;

public partial class DbContextGestionProyectos : DbContext
{
    public DbContextGestionProyectos(DbContextOptions<DbContextGestionProyectos> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividad> Actividad { get; set; }

    public virtual DbSet<Proyecto> Proyecto { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividad>(entity =>
        {
            entity.HasKey(e => e.ActividadId).HasName("PK__Activida__9814839039E6FB32");

            entity.Property(e => e.ActividadId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Estado).HasDefaultValue("ACTIVO");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Actividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actividad_Proyecto");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.ProyectoId).HasName("PK__Proyecto__CF241D65958C64E5");

            entity.Property(e => e.ProyectoId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Estado).HasDefaultValue("ACTIVO");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Proyecto).HasConstraintName("FK_Proyecto_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8508BA07E");

            entity.Property(e => e.UsuarioId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Estado).HasDefaultValue("ACTIVO");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
