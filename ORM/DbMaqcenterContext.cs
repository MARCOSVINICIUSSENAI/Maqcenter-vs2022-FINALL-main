using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MaqCenter.ORM;

public partial class DbMaqcenterContext : DbContext
{
    public DbMaqcenterContext()
    {
    }

    public DbMaqcenterContext(DbContextOptions<DbMaqcenterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAgendamento> TbAgendamentos { get; set; }

    public virtual DbSet<TbServico> TbServicos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    public virtual DbSet<ViewAgendamento> ViewAgendamentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB204_35\\SQLEXPRESS;Database=DB_MAQCENTER;User Id=admin;Password=admin;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAgendamento>(entity =>
        {
            entity.ToTable("TbAgendamento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtHoraAgendamento).HasColumnType("datetime");

            entity.HasOne(d => d.FkServicoNavigation).WithMany(p => p.TbAgendamentos)
                .HasForeignKey(d => d.FkServico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbAgendamento_TbServico");

            entity.HasOne(d => d.FkUsuarioNavigation).WithMany(p => p.TbAgendamentos)
                .HasForeignKey(d => d.FkUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbAgendamento_TbUsuario");
        });

        modelBuilder.Entity<TbServico>(entity =>
        {
            entity.ToTable("TbServico");

            entity.Property(e => e.TipoServico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("TbUsuario");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewAgendamento>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewAgendamento");

            entity.Property(e => e.DtHoraAgendamento).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoServico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
