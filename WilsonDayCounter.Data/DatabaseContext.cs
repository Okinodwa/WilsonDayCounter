using Microsoft.EntityFrameworkCore;
using System;
using WilsonDayCounter.Data.Entities;

namespace WilsonDayCounter.Data
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<VisitorLog> VisitorLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=WilsonDayCounter.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitorLog>()
                .Property(b => b.DateEntered)
                .HasDefaultValueSql("datetime('now')");
        }
    }
}
