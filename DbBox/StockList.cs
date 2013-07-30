using System.Collections.Generic;

namespace DbBox
{
    public class StockList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Stock> Stocks { get; set; }
        public Country Country { get; set; }
    }

    public class Sector
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Stock> Stocks { get; set; }
        public Country Country { get; set; }
    }

}