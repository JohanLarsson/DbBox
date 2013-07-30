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
            Clear();
            using (var context = new DummyContext())
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                context.Countries.AddOrUpdate(DummyFactory.Country);
                context.SaveChanges();
                Console.WriteLine("dummyContext.Countries.AddOrUpdate(_country); took: " + stopwatch.ElapsedMilliseconds + " ms");
                Assert.DoesNotThrow(() => context.Countries.Single(x => x.Id == DummyFactory.Country.Id));
            }
        }

        [Test]
        public void InsertEmptyListsTest()
        {
            Clear();
            using (var context = new DummyContext())
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                context.StockLists.Add(DummyFactory.List1);
                context.StockLists.Add(DummyFactory.List2);
                context.SaveChanges();
                Console.WriteLine("dummyContext.StockLists.AddOrUpdate(stockList); took: " + stopwatch.ElapsedMilliseconds + " ms");
            }
            AssertEf.AssertLists(DummyFactory.Lists);
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
            AssertEf.AssertStocks(DummyFactory.Stocks);
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
            AssertEf.AssertStocks(DummyFactory.StocksWithLists);
        }

        [Test]
        public void InsertStocksThenListsWithStocksTest()
        {
            Clear();
            using (var context = new DummyContext())
            {
                foreach (var stock in DummyFactory.Stocks)
                {
                    context.Stocks.Add(stock);
                }
                context.SaveChanges();
            }
            using (var context = new DummyContext())
            {
                foreach (var listWithStocks in DummyFactory.ListsWithStocks)
                {
                    foreach (var stock in listWithStocks.Stocks)
                    {
                        context.Stocks.Attach(stock);
                    }
                    context.StockLists.AddOrUpdate(listWithStocks);
                }
                context.SaveChanges();
            }
            AssertEf.AssertStocks(DummyFactory.StocksWithLists);
            AssertEf.AssertLists(DummyFactory.ListsWithStocks);
        }

        [Test,Explicit]
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
    }

    public static class AssertEf
    {
        public static void AssertStocks(Stock[] stockList)
        {
            using (var context = new DummyContext())
            {
                foreach (var expected in stockList)
                {
                    var actual = context.Stocks.Single(x => x.Id == expected.Id);
                    Assert.AreEqual(expected.Id, actual.Id);
                    if (expected.List == null)
                        Assert.IsNull(actual.List);
                    else
                        Assert.AreEqual(expected.List.Id, actual.List.Id);
                }
            }
        }

        public static void AssertLists(StockList[] lists)
        {
            using (var context = new DummyContext())
            {
                foreach (var expected in lists)
                {
                    var actual = context.StockLists.Single(x => x.Id == expected.Id);
                    Assert.AreEqual(expected.Id, actual.Id);
                    Assert.AreEqual(expected.Country.Id,actual.Country.Id);
                    if (expected.Stocks == null)
                        Assert.IsNull(actual.Stocks);
                    else
                    {
                        var expecteds = expected.Stocks.OrderBy(x => x.Id).Select(x => x.Id).ToArray();
                        var actuals = actual.Stocks.OrderBy(x => x.Id).Select(x => x.Id).ToArray();
                        Assert.IsTrue(expecteds.SequenceEqual(actuals));
                    }
                }
            }
        }
    }
}
