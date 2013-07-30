using System.Linq;
using DbBox;
using NUnit.Framework;

namespace DbBoxTests
{
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