using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork34
{
    class Employee
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public Position Position { get; private set; }
        public ICollection<DocumentStatus> DocumentStatuses { get; private set; }

        public Employee()
        {
            DocumentStatuses = new HashSet<DocumentStatus>();
        }

        public Employee(string fullName, Position position)
        {
            FullName = fullName;
            Position = position;
        }
    }
}
