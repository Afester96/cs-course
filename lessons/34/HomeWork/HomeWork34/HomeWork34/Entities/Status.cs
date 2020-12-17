using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork34
{
    class Status
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<DocumentStatus> DocumentStatuses { get; private set; }

        public Status()
        {
            DocumentStatuses = new HashSet<DocumentStatus>();
        }

        public Status(string name)
        {
            Name = name;
        }
    }
}
