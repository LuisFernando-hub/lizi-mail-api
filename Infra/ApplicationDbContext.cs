using lizi_mail_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace lizi_mail_api.Infra
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<UserEntity?> User { get; set; }
        public DbSet<EmailEntity?> Email { get; set; }
        public DbSet<ApiKeyEntity?> ApiKey { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Configure relationships and constraints using Fluent API
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.email)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.plan)
                .HasConversion<string>();


            modelBuilder.Entity<EmailEntity>()
                .Property(u => u.status)
                .HasConversion<string>();

            modelBuilder.Entity<EmailEntity>()
                .HasOne(e => e.user)
                .WithMany()
                .HasForeignKey(e => e.user_id)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Email_User");

            modelBuilder.Entity<EmailEntity>()
                .HasOne(e => e.api_key)
                .WithMany()
                .HasForeignKey(e => e.api_key_id)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Email_ApiKey");

            modelBuilder.Entity<ApiKeyEntity>()
                .HasOne(a => a.user)
                .WithMany()
                .HasForeignKey(a => a.user_id)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ApiKey_User");

        }
    }
}
