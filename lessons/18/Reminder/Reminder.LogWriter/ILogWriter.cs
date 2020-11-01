using Reminder.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.LogWriter
{
    public interface ILogWriter
    {
        void CreatedLog(object sender, ReminderEventArgs args);
        void ReadyLog(object sender, ReminderEventArgs args);
        void SentLog(object sender, ReminderEventArgs args);
        void FailedLog(object sender, ReminderEventArgs args);
    }
}
