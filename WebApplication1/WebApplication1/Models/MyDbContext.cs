using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class MyDbContext:DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer>   Customers { get; set; }
        public DbSet<Author>   Authors { get; set; }
    }
}
