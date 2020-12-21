using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork34
{
    class Address
    {
        public int Id { get; private set; }
        public City City { get; private set; }
        public string Street { get; private set; }
        public string House { get; private set; }
        public ICollection<DocumentStatus> SenderDocumentStatuses { get; private set; }
        public ICollection<DocumentStatus> ReceiverDocumentStatuses { get; private set; }

        public Address()
        {
            SenderDocumentStatuses = new HashSet<DocumentStatus>();
            ReceiverDocumentStatuses = new HashSet<DocumentStatus>();
        }

        public Address(City city, string street, string house)
        {
            City = city;
            Street = street;
            House = house;
        }
    }
}
