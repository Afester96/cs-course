using Reminder.Domain;
using Reminder.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.LogWriter
{
    public class LogWriter : AbstractLogWriter
    {
        public override void Write(object sender, ReminderEventArgs args)
        {
            var text = Message(args);
            Console.WriteLine(text);
        }
    }
}
