using ComidasTipicasDelSur.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Mesero> Mesero { get; set; }
        public DbSet<Supervisor> Supervisor { get; set; }
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<DetalleFactura> Detalle_Factura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entidad Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTE");

                entity.HasKey(c => c.Identificacion)
                      .HasName("PK_CLIENTE");

                entity.Property(c => c.Identificacion)
                      .HasColumnName("IDENTIFICACION")
                      .IsRequired();

                entity.Property(c => c.Nombre)
                      .HasColumnName("NOMBRE")
                      .IsRequired();

                entity.Property(c => c.Apellidos)
                      .HasColumnName("APELLIDOS")
                      .IsRequired();

                entity.Property(c => c.Direccion)
                      .HasColumnName("DIRECCION")
                      .IsRequired();

                entity.Property(c => c.Telefono)
                      .HasColumnName("TELEFONO")
                      .IsRequired();

                entity.HasMany(c => c.Facturas)
                      .WithOne(f => f.Cliente)
                      .HasForeignKey(f => f.IdCliente)
                      .HasPrincipalKey(c => c.Identificacion)
                      .HasConstraintName("FK_FACTURA_CLIENTE");
            });

            // Entidad Mesa
            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.ToTable("MESA");

                entity.HasKey(m => m.NroMesa)
                      .HasName("PK_MESA");

                entity.Property(m => m.NroMesa)
                      .HasColumnName("NROMESA")
                      .IsRequired();

                entity.Property(m => m.Nombre)
                      .HasColumnName("NOMBRE")
                      .IsRequired();

                entity.Property(m => m.Reservada)
                      .HasColumnName("RESERVADA")
                      .IsRequired();

                entity.Property(m => m.Puestos)
                      .HasColumnName("PUESTOS")
                      .IsRequired();

                entity.HasMany(m => m.Facturas)
                      .WithOne(f => f.Mesa)
                      .HasForeignKey(f => f.NroMesa)
                      .HasPrincipalKey(m => m.NroMesa)
                      .HasConstraintName("FK_FACTURA_MESA");
            });

            // Entidad Mesero
            modelBuilder.Entity<Mesero>(entity =>
            {
                entity.ToTable("MESERO");

                entity.HasKey(m => m.IdMesero)
                      .HasName("PK_MESERO");

                entity.Property(m => m.IdMesero)
                      .HasColumnName("IDMESERO")
                      .IsRequired();

                entity.Property(m => m.Nombres)
                      .HasColumnName("NOMBRES")
                      .IsRequired();

                entity.Property(m => m.Apellidos)
                      .HasColumnName("APELLIDOS")
                      .IsRequired();

                entity.Property(m => m.Edad)
                      .HasColumnName("EDAD")
                      .IsRequired();

                entity.Property(m => m.Antiguedad)
                      .HasColumnName("ANTIGUEDAD")
                      .IsRequired();

                entity.HasMany(m => m.Facturas)
                      .WithOne(f => f.Mesero)
                      .HasForeignKey(f => f.IdMesero)
                      .HasPrincipalKey(m => m.IdMesero)
                      .HasConstraintName("FK_FACTURA_MESERO");
            });

            // Entidad Supervisor
            modelBuilder.Entity<Supervisor>(entity =>
            {
                entity.ToTable("SUPERVISOR");

                entity.HasKey(s => s.IdSupervisor)
                      .HasName("PK_SUPERVISOR");

                entity.Property(s => s.IdSupervisor)
                      .HasColumnName("IDSUPERVISOR")
                      .IsRequired();

                entity.Property(s => s.Nombres)
                      .HasColumnName("NOMBRES")
                      .IsRequired();

                entity.Property(s => s.Apellidos)
                      .HasColumnName("APELLIDOS")
                      .IsRequired();

                entity.Property(s => s.Edad)
                      .HasColumnName("EDAD")
                      .IsRequired();

                entity.Property(s => s.Antiguedad)
                      .HasColumnName("ANTIGUEDAD")
                      .IsRequired();

                entity.HasMany(s => s.DetallesFactura)
                      .WithOne(d => d.Supervisor)
                      .HasForeignKey(d => d.IdSupervisor)
                      .HasPrincipalKey(s => s.IdSupervisor)
                      .HasConstraintName("FK_DETALLEFACTURA_SUPERVISOR");
            });

            // Entidad Factura
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("FACTURA");

                entity.HasKey(f => f.NroFactura)
                      .HasName("PK_FACTURA");

                entity.Property(f => f.NroFactura)
                      .HasColumnName("NROFACTURA")
                      .IsRequired();

                entity.Property(f => f.IdCliente)
                      .HasColumnName("IDCLIENTE")
                      .IsRequired();

                entity.Property(f => f.NroMesa)
                      .HasColumnName("NROMESA")
                      .IsRequired();

                entity.Property(f => f.IdMesero)
                      .HasColumnName("IDMESERO")
                      .IsRequired();

                entity.Property(f => f.Fecha)
                      .HasColumnName("FECHA")
                      .IsRequired();

                entity.HasOne(f => f.Cliente)
                      .WithMany(c => c.Facturas)
                      .HasForeignKey(f => f.IdCliente)
                      .HasPrincipalKey(c => c.Identificacion)
                      .HasConstraintName("FK_FACTURA_CLIENTE");

                entity.HasOne(f => f.Mesa)
                      .WithMany(m => m.Facturas)
                      .HasForeignKey(f => f.NroMesa)
                      .HasPrincipalKey(m => m.NroMesa)
                      .HasConstraintName("FK_FACTURA_MESA");

                entity.HasOne(f => f.Mesero)
                      .WithMany(m => m.Facturas)
                      .HasForeignKey(f => f.IdMesero)
                      .HasPrincipalKey(m => m.IdMesero)
                      .HasConstraintName("FK_FACTURA_MESERO");

                entity.HasMany(f => f.Detalles)
                      .WithOne(d => d.Factura)
                      .HasForeignKey(d => d.NroFactura)
                      .HasPrincipalKey(f => f.NroFactura)
                      .HasConstraintName("FK_DETALLEFACTURA_FACTURA");
            });

            // Entidad DetalleFactura
            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.ToTable("DETALLE_FACTURA");

                entity.HasKey(d => d.IdDetalleXFactura)
                      .HasName("PK_DETALLEFACTURA");

                entity.Property(d => d.IdDetalleXFactura)
                      .HasColumnName("IDDETALLEFACTURA")
                      .IsRequired();

                entity.Property(d => d.NroFactura)
                      .HasColumnName("NROFACTURA")
                      .IsRequired();

                entity.Property(d => d.IdSupervisor)
                      .HasColumnName("IDSUPERVISOR")
                      .IsRequired();

                entity.Property(d => d.Plato)
                      .HasColumnName("PLATO")
                      .IsRequired();

                entity.Property(d => d.Valor)
                      .HasColumnName("VALOR")
                      .HasPrecision(10, 2) // 👈 Solución aplicada aquí
                      .IsRequired();

                entity.HasOne(d => d.Factura)
                      .WithMany(f => f.Detalles)
                      .HasForeignKey(d => d.NroFactura)
                      .HasPrincipalKey(f => f.NroFactura)
                      .HasConstraintName("FK_DETALLEFACTURA_FACTURA");

                entity.HasOne(d => d.Supervisor)
                      .WithMany(s => s.DetallesFactura)
                      .HasForeignKey(d => d.IdSupervisor)
                      .HasPrincipalKey(s => s.IdSupervisor)
                      .HasConstraintName("FK_DETALLEFACTURA_SUPERVISOR");
            });
        }
    }
}
