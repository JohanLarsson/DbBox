using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbBox
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StockList List { get; set; }
        public Country Country { get; set; }
    }
}
