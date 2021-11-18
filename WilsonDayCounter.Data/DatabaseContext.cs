using Microsoft.EntityFrameworkCore;
using System;
using WilsonDayCounter.Data.Models;

namespace WilsonDayCounter.Data
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<VisitorLog> VisitorLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=WilsonDayCounter.db");
        }
    }
}
