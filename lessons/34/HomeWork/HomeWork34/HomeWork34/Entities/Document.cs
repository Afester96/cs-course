using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork34
{
    class Document
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public int Pages { get; private set; }
        public ICollection<DocumentStatus> DocumentStatuses { get; private set; }

        public Document()
        {
            DocumentStatuses = new HashSet<DocumentStatus>();
        }

        public Document(string name, int pages)
        {
            FirstName = name;
            Pages = pages;
        }
    }
}
