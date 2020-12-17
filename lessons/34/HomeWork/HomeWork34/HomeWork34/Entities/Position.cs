using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork34
{
    class Position
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Employee> Employees { get; private set; }

        public Position()
        {
            Employees = new HashSet<Employee>(); ;
        }

        public Position(string name) : this()
        {
            Name = name;
        }
    }
}
