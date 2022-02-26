using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Entities.Models
{
    public partial class TestsContext : DbContext
    {
        private const string logFilePath = "LogDb.txt";

        private StreamWriter logStream = new StreamWriter(logFilePath, append: false);

        public virtual DbSet<AnswerVariant> AnswerVariants { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Theory> Theories { get; set; }

        public TestsContext()
        {
        }

        public TestsContext(DbContextOptions<TestsContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = .\\SQLExpress; Initial Catalog = Tests; Integrated Security = true;")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }

            optionsBuilder.LogTo(logStream.WriteLine, new[] { RelationalEventId.CommandExecuted });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AnswerVariant>(entity =>
            {
                entity.ToTable("Answer Variants");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AnswerVariants)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Answer Variants_Questions");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Img).IsRequired();
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Questions_Tests");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Tests_Images");

                entity.HasOne(d => d.Theory)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TheoryId)
                    .HasConstraintName("FK_Tests_Theories");
            });

            modelBuilder.Entity<Theory>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Url).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override void Dispose()
        {
            base.Dispose();
            logStream.Dispose();
        }
    }
}
