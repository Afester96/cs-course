using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork34
{
    class City
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Address> Addresses { get; private set; }

        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public City(string name) : this()
        {
            Name = name;
        }
    }
}
