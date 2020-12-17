using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork34
{
    class DocumentStatus
    {
        public int Id { get; private set; }
        public Document Document { get; private set; }
        public Employee SenderEmployee { get; private set; }
        public Address SenderAddress { get; private set; }
        public Employee ReceiverEmployee { get; private set; }
        public Address ReceiverAddress { get; private set; }
        public Status Status { get; private set; }
        public DateTimeOffset DateTime { get; private set; }

        public DocumentStatus(Document document, Employee senderEmployee, Address senderAdress, Employee receiverEmployee, 
            Address receiverAddress, Status status, DateTimeOffset dateTime)
        {
            Document = document;
            SenderEmployee = senderEmployee;
            SenderAddress = senderAdress;
            ReceiverEmployee = receiverEmployee;
            ReceiverAddress = receiverAddress;
            Status = status;
            DateTime = dateTime;
        }

        public DocumentStatus()
        {

        }
    }
}
