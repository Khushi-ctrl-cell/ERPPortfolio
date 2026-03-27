using System.Data.Entity;
using ERPPortfolio.Models;

namespace ERPPortfolio.Data
{
    public class ERPDbContext : DbContext
    {
        public ERPDbContext() : base("ERPDbContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductItem>()
                .Property(item => item.Weight)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductItem>()
                .HasOptional(item => item.ParentItem)
                .WithMany(item => item.ChildItems)
                .HasForeignKey(item => item.ParentItemId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
