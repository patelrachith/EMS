using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EMS.Models
{
    public partial class EMSContext : DbContext
    {
        public EMSContext(DbContextOptions<EMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=V2MUMLP0499\\SQLEXPRESS;Database=EMS;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DOB)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.DOJ)
                    .HasColumnType("date")
                    .HasColumnName("DOJ");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<EmployeeDepartment>(entity =>
            {
                entity.HasKey(e => e.MapId);

                entity.ToTable("EmployeeDepartment");

                entity.Property(e => e.MapId).HasColumnName("MapID");

                entity.Property(e => e.DateOfAssignment).HasColumnType("date");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.EmployeeDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_EmployeeDepartment_Department");

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.EmployeeDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_EmployeeDepartment_EmployeeDepartment");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
