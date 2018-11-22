using System.Data.Entity;

namespace Goggles.Models
{
    public class GogglesEntities : DbContext
    {
        public GogglesEntities()
            : base("GogglesEntities")
        { }
        public DbSet<Goggles.Models.Item> Items { get; set; }
        public DbSet<Goggles.Models.Category> Categories { get; set; }
        public DbSet<Goggles.Models.Producer> Producers { get; set; }
        public DbSet<Goggles.Models.Order> Orders { get; set; }
        public DbSet<Goggles.Models.OrderDetail> OrderDetails { get; set; }
        public DbSet<Goggles.Models.Cart> Carts { get; set; }
    }
}