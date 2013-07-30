using DbBox;

namespace DbBoxTests
{
    public class DummyFactory
    {
        private static Country _dummyCountry;
        public static Country Country
        {
            get { return _dummyCountry ?? (_dummyCountry = new Country(id: "XX_countryId_XX", name: "XX_CountryName_XX")); }
        }

        private static StockList _dummyList1;
        public static StockList List1
        {
            get { return _dummyList1 ?? (_dummyList1 = new StockList { Country = Country, Id = "XX_List1Id_XX", Name = "XX_List1Name_XX" }); }
        }

        private static StockList _dummyList2;
        public static StockList List2
        {
            get { return _dummyList2 ?? (_dummyList2 = new StockList { Country = Country, Id = "XX_List2Id_XX", Name = "XX_List2Name_XX" }); }
        }

        public static StockList[] Lists
        {
            get { return new[] { List1, List2 }; }
        }

        private static StockList[] _listsWithStocks;
        public static StockList[] ListsWithStocks
        {
            get
            {
                if (_listsWithStocks == null)
                {
                    var list1 = List1;
                    list1.Stocks.Add(Stock11);
                    list1.Stocks.Add(Stock12);
                    var list2 = List2;
                    list2.Stocks.Add(Stock21);
                    list2.Stocks.Add(Stock22);
                    _listsWithStocks = new[] { list1, list2 };
                }
                return _listsWithStocks;
            }
        }

        private static Stock _stock11;
        public static Stock Stock11
        {
            get { return _stock11 ?? (_stock11 = new Stock { Id = 99911999, Name = "XX_Stock11Name_XX" }); }
        }

        private static Stock _stock12;
        public static Stock Stock12
        {
            get { return _stock12 ?? (_stock12 = new Stock { Id = 99912999, Name = "XX_Stock12Name_XX" }); }
        }

        private static Stock _stock21;
        public static Stock Stock21
        {
            get { return _stock21 ?? (_stock21 = new Stock { Id = 99921999, Name = "XX_Stock21Name_XX" }); }
        }

        private static Stock _stock22;
        public static Stock Stock22
        {
            get { return _stock22 ?? (_stock22 = new Stock { Id = 99922999, Name = "XX_Stock22Name_XX" }); }
        }

        public static Stock[] Stocks
        {
            get
            {
                return new[] { Stock11, Stock12, Stock21, Stock22 };
            }
        }

        private static Stock[] _stocksWithLists;
        public static Stock[] StocksWithLists
        {
            get
            {
                if (_stocksWithLists == null)
                {
                    var stock11 = Stock11;
                    stock11.List = List1;
                    var stock12 = Stock12;
                    stock12.List = List1;
                    var stock21 = Stock21;
                    stock21.List = List2;
                    var stock22 = Stock22;
                    stock22.List = List2;
                    _stocksWithLists = new[] { stock11, stock12, stock21, stock22 };
                }
                return _stocksWithLists;
            }
        }
    }
}