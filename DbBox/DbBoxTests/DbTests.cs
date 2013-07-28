using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbBox;
using NUnit.Framework;

namespace DbBoxTests
{
    public class DbTests
    {
        [Test]
        public void InsertTest()
        {
            using (var dummyContext = new DummyContext())
            {
                var country = new Country() { Id = "SE", Name = "Sweden" };
                if (!dummyContext.Countries.Any(x => x.Id == country.Id))
                {
                    dummyContext.Countries.Add(country);
                    dummyContext.SaveChanges();
                }
                country = dummyContext.Countries.Single(x => x.Id == "SE");
                var stockList = new StockList() { Country = country, Id = "List1", Name = "List1Name" };
                //dummyContext.Countries.Single(x=>x.Id==country.Id).Lists.Add(stockList);

                dummyContext.StockLists.Add(stockList);
                dummyContext.SaveChanges();
            }
        }

        [Test]
        public void GetTest()
        {
            using (var dummyContext = new DummyContext())
            {
                List<Country> countries = dummyContext.Countries.ToList();
                Assert.AreEqual(1, countries.Count());
            }
        }
    }
}
