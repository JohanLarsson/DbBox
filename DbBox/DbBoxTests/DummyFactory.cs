using DbBox;

namespace DbBoxTests
{
    public class DummyFactory
    {
        private static Country _dummyCountry;
        public static Country Country
        {
            get
            {
                if (_dummyCountry == null)
                    _dummyCountry = new Country { Id = "XX_countryId_XX", Name = "XX_CountryName_XX" };
                return _dummyCountry;
            }
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

        public static Stock[] StocksWithLists
        {
            get
            {
                var stock11 = Stock11;
                stock11.List = List1;
                var stock12 = Stock12;
                stock12.List = List1;
                var stock21 = Stock21;
                stock21.List = List2;
                var stock22 = Stock22;
                stock22.List = List2;
                return new[] { stock11, stock12, stock21, stock22 };
            }
        }
    }
}