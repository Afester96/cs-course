using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Storage
{
    public class ReminderItemFilter
    {
        public DateTimeOffset? DateTime { get; }
        public ReminderItemStatus? Status { get; }
        public ReminderItemFilter(DateTimeOffset? dateTime, ReminderItemStatus? status)
        {
            DateTime = dateTime;
            Status = status;
        }

        public static ReminderItemFilter ByStatus(ReminderItemStatus status) =>
            new ReminderItemFilter(default, status);

        public static ReminderItemFilter CreatedAt(DateTimeOffset dateTime) =>
            new ReminderItemFilter(dateTime, ReminderItemStatus.Created);
    }
}
