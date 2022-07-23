using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CajaMorelia.Models;

namespace CajaMorelia.Data
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TipoCuenta> TipoCuenta { get; set; } = null!;
        public virtual DbSet<Cliente> Cliente { get; set; } = null!;
        public virtual DbSet<ClienteCuenta> ClienteCuenta { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<TipoCuenta>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PRIMARY");

                entity.ToTable("cat_cmv_tipo_cuenta");

                entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");

                entity.Property(e => e.NombreCuenta)
                    .HasMaxLength(45)
                    .HasColumnName("nombre_cuenta");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cmv_cliente");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(45)
                    .HasColumnName("apellido_materno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(45)
                    .HasColumnName("apellido_paterno");

                entity.Property(e => e.Curp)
                    .HasMaxLength(18)
                    .HasColumnName("curp");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_alta")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(45)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(13)
                    .HasColumnName("rfc");
            });

            modelBuilder.Entity<ClienteCuenta>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.IdCuenta, e.IdClienteCuenta })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("tbl_cmv_cliente_cuenta");

                entity.HasIndex(e => e.IdCuenta, "fk_TBL_CMV_CLIENTE_has_CAT_CMV_TIPO_CUENTA_CAT_CMV_TIPO_CUE_idx");

                entity.HasIndex(e => e.IdCliente, "fk_TBL_CMV_CLIENTE_has_CAT_CMV_TIPO_CUENTA_TBL_CMV_CLIENTE_idx");

                entity.Property(e => e.IdClienteCuenta).HasColumnName("id_cliente_cuenta");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");

                entity.Property(e => e.FechaContratacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_contratacion")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FechaUltimoMovimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ultimo_movimiento");

                entity.Property(e => e.SaldoActual)
                    .HasPrecision(15, 2)
                    .HasColumnName("saldo_actual");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
