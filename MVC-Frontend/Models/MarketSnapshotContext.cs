using System.Data.Entity;

namespace MVC_Frontend.Models
{
    public class MarketSnapshotContext : DbContext
    {
        public DbSet<MarketSnapshot> MarketSnapshots { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}