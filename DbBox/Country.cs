using System.Collections.Generic;

namespace DbBox
{
    public class Country
    {
        private Country() { }
        public Country(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }

        private readonly ISet<StockList> _lists = new HashSet<StockList>();
        public virtual ISet<StockList> Lists
        {
            get { return _lists; }
        }

        private readonly ISet<Sector> _sectors = new HashSet<Sector>();
        public virtual ISet<Sector> Sectors
        {
            get { return _sectors; }
        }
    }
}