using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Storage.WebApi.Dto
{
    class ReminderItemDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public ReminderItemStatus Status { get; set; }
        public string Message { get; set; }
        public string ContactId { get; set; }
    }
}
