using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kinoteatr.Models;

public partial class KinoteatrContext : DbContext
{
    public KinoteatrContext()
    {
    }

    public KinoteatrContext(DbContextOptions<KinoteatrContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mestum> Mesta { get; set; }

    public virtual DbSet<Typemest> Typemests { get; set; }

    public virtual DbSet<Zabronmestum> Zabronmesta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server = localhost; initial catalog = Kinoteatr; trusted_connection = true; TrustServerCertificate = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mestum>(entity =>
        {
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("status");

            entity.HasOne(d => d.Type).WithMany(p => p.Mesta)
                .HasForeignKey(d => d.Typeid)
                .HasConstraintName("FK_Mesta_typemest");
        });

        modelBuilder.Entity<Typemest>(entity =>
        {
            entity.ToTable("typemest");

            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Zabronmestum>(entity =>
        {
            entity.ToTable("zabronmesta");

            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("status");

            entity.HasOne(d => d.Mesta).WithMany(p => p.Zabronmesta)
                .HasForeignKey(d => d.Mestaid)
                .HasConstraintName("FK_zabronmesta_Mesta");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
