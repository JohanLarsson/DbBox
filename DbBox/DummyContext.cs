using System;
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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(x => x.Id);
            modelBuilder.Entity<Country>().HasMany(x => x.Lists);

            modelBuilder.Entity<StockList>().HasKey(x => x.Id);
            modelBuilder.Entity<StockList>().HasRequired(x => x.Country);
            base.OnModelCreating(modelBuilder);
        }
    }
}
