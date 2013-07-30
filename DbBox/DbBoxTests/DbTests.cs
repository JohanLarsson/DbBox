using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
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
            Country country = new Country { Id = "XX_countryId_XX", Name = "XX_CountryName_XX" };
            StockList stockList = new StockList { Country = country, Id = "XX_listId_XX", Name = "XX_List1Name_XX" };
            using (var dummyContext = new DummyContext())
            {
                const int stockId = 123;
                Stopwatch stopwatch = Stopwatch.StartNew();

                dummyContext.Countries.AddOrUpdate(country);
                dummyContext.SaveChanges();
                Console.WriteLine("dummyContext.Countries.AddOrUpdate(_country); took: " + stopwatch.ElapsedMilliseconds + " ms");

                stopwatch.Restart();

                dummyContext.StockLists.AddOrUpdate(stockList);
                dummyContext.SaveChanges();
                Console.WriteLine("dummyContext.StockLists.AddOrUpdate(stockList); took: " + stopwatch.ElapsedMilliseconds + " ms");

                stopwatch.Restart();
                var stock = new Stock { Id = stockId, List = stockList, Name = "Stock123Name" };
                dummyContext.Stocks.AddOrUpdate(stock);
                dummyContext.SaveChanges();
                Console.WriteLine("dummyContext.Stocks.AddOrUpdate(stock) took: " + stopwatch.ElapsedMilliseconds + " ms");
                Assert.DoesNotThrow(() => dummyContext.Countries.Single(x => x.Id == country.Id));
                Assert.AreEqual(country.Id, dummyContext.StockLists.Single(x => x.Id == stockList.Id).Country.Id);

                Assert.DoesNotThrow(() => dummyContext.StockLists.Single(x => x.Id == stockList.Id));
                Assert.DoesNotThrow(() => dummyContext.Countries.Single(x => x.Id == country.Id).Lists.Single(x => x.Id == stockList.Id));

                Assert.AreEqual(stockId, dummyContext.Stocks.Single().Id);
                Assert.AreEqual(stockId, dummyContext.StockLists.Single().Stocks.Single().Id);
                Assert.AreEqual(stockList.Id, dummyContext.Stocks.Single(x => x.Id == stock.Id).List.Id);
            }
        }

        [Test]
        public void LoopAddCountriesTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 10; i++)
            {
                stopwatch.Restart();
                using (var dummyContext = new DummyContext())
                {
                    dummyContext.Countries.Add(new Country { Id = i.ToString(), Name = i.ToString() });
                    dummyContext.SaveChanges();
                }
                Console.WriteLine("Adding {0} took {1} ms", i, stopwatch.ElapsedMilliseconds);

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
