using System;

namespace HomeWork22.City
{
    public interface ICity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Population { get; set; }
    }
}
