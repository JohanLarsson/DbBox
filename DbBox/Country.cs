using System.Collections.Generic;

namespace DbBox
{
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<StockList> Lists { get; set; }
        //public List<Stock> Stocks { get; set; } 
    }
}