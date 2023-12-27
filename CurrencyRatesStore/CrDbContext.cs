using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRatesStore
{
    public class CrDbContext : DbContext
    {
       public DbSet<CurrencyRate> Currencies { get; set; }
       public CrDbContext() 
        {  
        //this.Database.EnsureDeleted();
        this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = currencyRates.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
