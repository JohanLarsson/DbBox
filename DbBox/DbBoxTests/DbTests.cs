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
                const string countryId = "SE";
                string listId = "ListId";
                if (!dummyContext.Countries.Any(x => x.Id == countryId))
                {
                    var country = new Country() { Id = countryId, Name = "Sweden" };
                    dummyContext.Countries.Add(country);
                    dummyContext.SaveChanges();
                }
                if (!dummyContext.StockLists.Any(x => x.Id == listId))
                {
                    var country = dummyContext.Countries.Single(x => x.Id == countryId);
                    var stockList = new StockList() { Country = country, Id = listId, Name = "List1Name" };
                    dummyContext.StockLists.Add(stockList);
                    dummyContext.SaveChanges();
                }
                Assert.AreEqual(countryId, dummyContext.Countries.Single().Id);
                Assert.AreEqual(countryId, dummyContext.StockLists.Single().Country.Id);
                Assert.AreEqual(listId, dummyContext.StockLists.Single().Id);
                Assert.AreEqual(listId,dummyContext.Countries.Single().Lists.Single().Id);
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
