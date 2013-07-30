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
        public void InsertCountryTest()
        {
            using (var context = new DummyContext())
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                context.Countries.AddOrUpdate(DummyFactory.Country);
                context.SaveChanges();
                Console.WriteLine("dummyContext.Countries.AddOrUpdate(_country); took: " + stopwatch.ElapsedMilliseconds + " ms");
                Assert.DoesNotThrow(() => context.Countries.Single(x => x.Id == DummyFactory.Country.Id));
                stopwatch.Restart();
                stopwatch.Restart();
            }
        }

        [Test]
        public void InsertListsTest()
        {
            using (var context = new DummyContext())
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                context.StockLists.AddOrUpdate(DummyFactory.List1);
                context.StockLists.AddOrUpdate(DummyFactory.List2);
                context.SaveChanges();
                Console.WriteLine("dummyContext.StockLists.AddOrUpdate(stockList); took: " + stopwatch.ElapsedMilliseconds + " ms");
                Assert.DoesNotThrow(() => context.StockLists.Single(x => x.Id == DummyFactory.List1.Id));
                Assert.DoesNotThrow(() => context.StockLists.Single(x => x.Id == DummyFactory.List2.Id));
                Assert.AreEqual(DummyFactory.Country.Id, context.StockLists.Single(x => x.Id == DummyFactory.List1.Id).Country.Id);
                Assert.AreEqual(DummyFactory.Country.Id, context.StockLists.Single(x => x.Id == DummyFactory.List2.Id).Country.Id);
            }
        }

        [Test]
        public void InsertStocksWithoutListsTest()
        {
            Clear();
            using (var context = new DummyContext())
            {
                foreach (var stock in DummyFactory.Stocks)
                {
                    context.Stocks.AddOrUpdate(stock);
                }
                context.SaveChanges();
            }
            using (var context = new DummyContext())
            {
                foreach (var stock in DummyFactory.Stocks)
                {
                    var actual = context.Stocks.Single(x => x.Id == stock.Id);
                    Assert.AreEqual(stock.Id, actual.Id);
                    Assert.IsNull(actual.List);
                }
            }
        }

        [Test]
        public void InsertStocksWithListsTest()
        {
            Clear();
            using (var context = new DummyContext())
            {
                foreach (var stock in DummyFactory.StocksWithLists)
                {
                    context.Stocks.AddOrUpdate(stock);
                }
                context.SaveChanges();
            }
            using (var context = new DummyContext())
            {
                foreach (var stock in DummyFactory.StocksWithLists)
                {
                    var actual = context.Stocks.Single(x => x.Id == stock.Id);
                    Assert.AreEqual(stock.Id, actual.Id);
                    Assert.AreEqual(stock.List.Id, actual.List.Id);
                }
            }
        }

        private static void Clear()
        {
            using (var context = new DummyContext())
            {
                var country = context.Countries.SingleOrDefault(x => x.Id == DummyFactory.Country.Id);
                if (country != null)
                    context.Countries.Remove(country);

                foreach (var stockList in DummyFactory.Lists)
                {
                    var removeList = context.StockLists.SingleOrDefault(x => x.Id == stockList.Id);
                    if (removeList != null)
                        context.StockLists.Remove(removeList);
                }

                foreach (var stock in DummyFactory.Stocks)
                {
                    var removeStock = context.Stocks.SingleOrDefault(x => x.Id == stock.Id);
                    if (removeStock != null)
                        context.Stocks.Remove(removeStock);
                }
                context.SaveChanges();
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
