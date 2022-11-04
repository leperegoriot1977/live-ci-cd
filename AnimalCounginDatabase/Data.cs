using Microsoft.EntityFrameworkCore;

namespace AnimalCountinDatabase
{
    public class Customer
    {
        public int Id { get; set; }

        public string? Name { get; set; }
    }

    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }

        public DbSet<Customer>? Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
