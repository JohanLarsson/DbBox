using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbBox
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual StockList List { get; set; }
        public virtual Sector Sector { get; set; }
    }
}
