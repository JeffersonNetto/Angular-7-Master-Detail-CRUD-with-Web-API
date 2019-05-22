using Microsoft.EntityFrameworkCore;

namespace RestaurantWebApi.Models
{
    public class DBModel : DbContext
    {
        public DBModel(DbContextOptions<DBModel> options) : base(options)
        {            
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
    }
}
