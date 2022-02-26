using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ComputerShop.App.Models
{
    public partial class ComputerShopContext : DbContext
    {
        public ComputerShopContext()
        {
        }

        public ComputerShopContext(DbContextOptions<ComputerShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<BasketElement> BasketElements { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Subcatalog> Subcatalogs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = .\\sqlexpress; Database = ComputerStore; Trusted_Connection = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BasketEmentId })
                    .HasName("PK_Basket");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.BasketEment)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.BasketEmentId)
                    .HasConstraintName("FK_Baskets_BasketElements");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Basket)
                    .HasForeignKey<Basket>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Baskets_Users");
            });

            modelBuilder.Entity<BasketElement>(entity =>
            {
                entity.HasOne(d => d.Goods)
                    .WithMany(p => p.BasketElements)
                    .HasForeignKey(d => d.GoodsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasketElements_Goods");
            });

            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Good>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.Subcatalog)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.SubcatalogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Goods_Subcatalogs");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Subcatalog>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.Catalog)
                    .WithMany(p => p.Subcatalogs)
                    .HasForeignKey(d => d.CatalogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subcatalogs_Catalogs");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
