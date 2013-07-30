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
        protected Stock() { }

        public Stock(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public virtual StockList List { get; set; }
        public virtual Sector Sector { get; private set; }
    }
}
