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
        private const string _countryId = "XX_countryId_XX";
        private const string _countryName = "XX_CountryName_XX";
        private const string _List1Id = "XX_List1Id_XX";
        private const string _list1Name = "XX_List1Name_XX";
        private const string _List2Id = "XX_List2Id_XX";
        private const string _list2Name = "XX_List2Name_XX";
        private const int stock11Id = 99911999;
        private const string _stock11Name = "XX_Stock11Name_XX";
        private const int stock12Id = 99912999;
        private const string _stock12Name = "XX_Stock12Name_XX";
        private const int stock21Id = 99921999;
        private const string _stock21Name = "XX_Stock21Name_XX";
        private const int stock22Id = 99922999;
        private const string _stock22Name = "XX_Stock22Name_XX";
        [Test]
        public void InsertTest()
        {
            Country country = new Country { Id = _countryId, Name = _countryName };
            StockList stockList1 = new StockList { Country = country, Id = _List1Id, Name = _list1Name };
            StockList stockList2 = new StockList { Country = country, Id = _List2Id, Name = _list2Name };
            var stock11 = new Stock { Id = stock11Id, List = stockList1, Name = _stock11Name };
            var stock12 = new Stock { Id = stock12Id, List = stockList1, Name = _stock12Name };
            var stock21 = new Stock { Id = stock21Id, List = stockList2, Name = _stock21Name };
            var stock22 = new Stock { Id = stock22Id, List = stockList2, Name = _stock22Name };
            using (var dummyContext = new DummyContext())
            {

                Stopwatch stopwatch = Stopwatch.StartNew();
                dummyContext.Countries.AddOrUpdate(country);
                dummyContext.SaveChanges();
                Console.WriteLine("dummyContext.Countries.AddOrUpdate(_country); took: " + stopwatch.ElapsedMilliseconds + " ms");
                stopwatch.Restart();

                dummyContext.StockLists.AddOrUpdate(stockList1);
                dummyContext.StockLists.AddOrUpdate(stockList2);
                dummyContext.SaveChanges();
                Console.WriteLine("dummyContext.StockLists.AddOrUpdate(stockList); took: " + stopwatch.ElapsedMilliseconds + " ms");

                stopwatch.Restart();
                dummyContext.Stocks.AddOrUpdate(stock11);
                dummyContext.Stocks.AddOrUpdate(stock12);
                dummyContext.Stocks.AddOrUpdate(stock21);
                dummyContext.Stocks.AddOrUpdate(stock22);
                dummyContext.SaveChanges();
                Console.WriteLine("dummyContext.Stocks.AddOrUpdate(stock) took: " + stopwatch.ElapsedMilliseconds + " ms");
                Assert.DoesNotThrow(() => dummyContext.Countries.Single(x => x.Id == country.Id));
                Assert.AreEqual(country.Id, dummyContext.StockLists.Single(x => x.Id == stockList1.Id).Country.Id);

                Assert.DoesNotThrow(() => dummyContext.StockLists.Single(x => x.Id == stockList1.Id));
                Assert.DoesNotThrow(() => dummyContext.Countries.Single(x => x.Id == country.Id).Lists.Single(x => x.Id == stockList1.Id));

                Assert.AreEqual(stock11Id, dummyContext.Stocks.Single(x => x.Id == stock11Id).Id);
                Assert.AreEqual(stock11Id, dummyContext.StockLists.Single(x => x.Id == _List1Id).Stocks.Single(x => x.Id == stock11Id).Id);
                Assert.AreEqual(stockList1.Id, dummyContext.Stocks.Single(x => x.Id == stock12.Id).List.Id);
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
