using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AutoService.Models
{
    public partial class AutoServiceContext : DbContext
    {
        public AutoServiceContext()
        {
        }

        public AutoServiceContext(DbContextOptions<AutoServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AutoMark> AutoMarks { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkKind> WorkKinds { get; set; }
        public virtual DbSet<WorkSubkind> WorkSubkinds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = .\\sqlexpress; Database = AutoService; Trusted_Connection = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AutoMark>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("nvarchar(4000)");

                entity.Property(e => e.DescriptionTitle)
                    .IsRequired()
                    .HasColumnType("nvarchar(1000)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.Lgn, e.Password })
                    .HasName("PK_User");

                entity.Property(e => e.Lgn).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Roles");
            });

            modelBuilder.Entity<WorkKind>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("nvarchar(4000)");

                entity.Property(e => e.DescriptionTitle).HasColumnType("nvarchar(1000)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<WorkSubkind>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("nvarchar(4000)");

                entity.Property(e => e.DescriptionTitle)
                    .IsRequired()
                    .HasColumnType("nvarchar(1000)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.WorkKind)
                    .WithMany(p => p.WorkSubkinds)
                    .HasForeignKey(d => d.WorkKindId)
                    .HasConstraintName("FK_WorkSubkinds_WorkKinds");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
