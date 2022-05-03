using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Telcel.R9.Estructura.AccesoDatos
{
    public partial class IEspinozaTelcelR9EstructuraContext : DbContext
    {
        public IEspinozaTelcelR9EstructuraContext()
        {
        }

        public IEspinozaTelcelR9EstructuraContext(DbContextOptions<IEspinozaTelcelR9EstructuraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Puesto> Puestos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<VistaEmpleado> VistaEmpleados { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-VA31VKK7; Database= IEspinozaTelcelR9Estructura; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleado");

                entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");

                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PuestoId).HasColumnName("PuestoID");

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.DepartamentoId)
                    .HasConstraintName("FK__Empleado__Depart__15502E78");

                entity.HasOne(d => d.Puesto)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.PuestoId)
                    .HasConstraintName("FK__Empleado__Puesto__145C0A3F");
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.ToTable("Puesto");

                entity.Property(e => e.PuestoId).HasColumnName("PuestoID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Usuario");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VistaEmpleado>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VistaEmpleado");

                entity.Property(e => e.Departamento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");

                entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Puesto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PuestoId).HasColumnName("PuestoID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
