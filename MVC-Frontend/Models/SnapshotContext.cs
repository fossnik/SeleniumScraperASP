﻿using System.Data.Entity;

namespace MVC_Frontend.Models
{
    class SnapshotContext : DbContext
    {
        public DbSet<MarketSnapshot> MarketSnapshots { get; set; }
        public DbSet<Coin> Coins { get; set; }
    }
}