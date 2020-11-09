using Microsoft.EntityFrameworkCore;

namespace Eshop.GraphQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            modelBuilder
                .Entity<UserOrder>()
                .HasKey(uo => new { uo.UserId, uo.OrderId });

            modelBuilder
                .Entity<OrderItem>()
                .HasOne<Order>(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
    }
}