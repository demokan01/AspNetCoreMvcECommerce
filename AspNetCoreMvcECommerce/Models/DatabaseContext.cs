using Microsoft.EntityFrameworkCore;


namespace AspNetCoreMvcECommerce.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<RoleAccount> RoleAccounts { get; set; }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<SlideShow> SlideShows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);

                entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);

                entity.HasOne(e => e.Parent)
                .WithMany(p => p.InverseParents)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Category_Category");
                
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            });

            modelBuilder.Entity<RoleAccount>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.AccountId });

                entity.HasOne(d => d.Account)
                .WithMany(p => p.RoleAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Role_Account_Account");

                entity.HasOne(d => d.Role)
                .WithMany(p => p.RoleAccounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Role_Account_Role");
            });
        }

    }
}
