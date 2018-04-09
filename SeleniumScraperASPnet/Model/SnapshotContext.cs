using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumScraperASPnet.Model
{
    class SnapshotContext : DbContext
    {
        public DbSet<MarketSnapshot> MarketSnapshots { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}
