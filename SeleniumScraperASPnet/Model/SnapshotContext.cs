using System.Data.Entity;

namespace SeleniumScraperASPnet.Model
{
    class SnapshotContext : DbContext
    {
        public DbSet<MarketSnapshot> MarketSnapshots { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}