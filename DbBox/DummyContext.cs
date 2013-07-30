using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbBox
{
    public class DummyContext :DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<StockList> StockLists { get; set; }
        public DbSet<Stock> Stocks { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(x => x.Id);
            modelBuilder.Entity<Country>().HasMany(x => x.Lists);
            //modelBuilder.Entity<Country>().HasMany(x => x.Stocks);

            modelBuilder.Entity<StockList>().HasKey(x => x.Id);
            modelBuilder.Entity<StockList>().HasRequired(x => x.Country);
            modelBuilder.Entity<StockList>().HasMany(x => x.Stocks);

            modelBuilder.Entity<Stock>().HasKey(x => x.Id);
            modelBuilder.Entity<Stock>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Stock>().HasRequired(x => x.List);
            //modelBuilder.Entity<Stock>().HasRequired(x => x.Country);
            base.OnModelCreating(modelBuilder);
        }
    }
}
