using System.Collections.Generic;

namespace DbBox
{
    public class Sector
    {
        private Sector()
        {

        }
        public string Id { get; set; }
        public string Name { get; set; }
        private readonly ISet<Stock> _stocks = new HashSet<Stock>();
        public virtual ISet<Stock> Stocks
        {
            get { return _stocks; }
        }

        public virtual Country Country { get; set; }
    }
}