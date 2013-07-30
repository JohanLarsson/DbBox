using System.Collections.Generic;

namespace DbBox
{
    public class Sector
    {
        protected Sector() { }

        public Sector(string id, string name, Country country)
        {
            Id = id;
            Name = name;
            Country = country;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        private readonly ISet<Stock> _stocks = new HashSet<Stock>();
        public virtual ISet<Stock> Stocks
        {
            get { return _stocks; }
        }

        public virtual Country Country { get; private set; }
    }
}