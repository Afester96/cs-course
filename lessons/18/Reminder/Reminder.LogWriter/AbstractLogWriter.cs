using Reminder.Domain;
using Reminder.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.LogWriter
{
    public abstract class AbstractLogWriter : ILogWriter
    {
        public abstract void Write(object sender, ReminderEventArgs args);
        public void CreatedLog(object sender, ReminderEventArgs args)
        {
            Write(sender, args);
        }

        public void FailedLog(object sender, ReminderEventArgs args)
        {
            Write(sender, args);
        }

        public void ReadyLog(object sender, ReminderEventArgs args)
        {
            Write(sender, args);
        }

        public void SentLog(object sender, ReminderEventArgs args)
        {
            Write(sender, args);
        }

        protected string Message(ReminderEventArgs args) => 
            $"Reminder ({args.Reminder.Id}) at " +
                $"{args.Reminder.DateTime:F} with " +
                $"message {args.Reminder.Message}";
    }
}
