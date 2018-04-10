using System.Data.Entity;

namespace MVC_Frontend.Models
{
    public class SnapshotContext : DbContext
    {
        public DbSet<MarketSnapshot> MarketSnapshots { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}