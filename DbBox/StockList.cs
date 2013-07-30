using System.Collections.Generic;

namespace DbBox
{
    public class StockList
    {
        public StockList()
        {
            Stocks= new List<Stock>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual List<Stock> Stocks { get; set; }
        public virtual Country Country { get; set; }
    }

    public class Sector
    {
        public Sector()
        {
            Stocks= new List<Stock>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual List<Stock> Stocks { get; set; }
        public virtual Country Country { get; set; }
    }

}