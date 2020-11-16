using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork22.Storage
{
    class Storage : IStorage
    {
        private readonly Dictionary<Guid, City> _cityes;

        public Storage()
        {
            _cityes = new Dictionary<Guid, City>();
        }
        
        public void Add(City city)
        {
            if (_cityes.ContainsKey(city.Id))
            {
                throw new Exception("Allready exist");
            }
            _cityes.Add(city.Id, city);
        }

        public void Delete(City city)
        {
            if (!_cityes.ContainsKey(city.Id))
            {
                throw new Exception("Not exist");
            }
            _cityes[city.Id] = city;
        }

        public City Get(Guid id)
        {
            if (!_cityes.TryGetValue(id, out var city))
            {
                throw new Exception("Not found");
            }
            return city;
        }

        public void Update(City city)
        {
            if (!_cityes.ContainsKey(city.Id))
            {
                throw new Exception("Not found");
            }
            _cityes[city.Id] = city;
        }
    }
}
