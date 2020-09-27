using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork11
{
    partial class ReminderItem
    {
        public ReminderItem(DateTimeOffset alarmDate, string alarmMessage)
        {
            AlarmDate = alarmDate;
            AlarmMessage = alarmMessage;
        }
        public virtual string Description =>
            $"{GetType()}\nAlarm Date: {AlarmDate}, Alarm Message: {AlarmMessage}, \nTime To Alarm: {TimeToAlarm}, Is Outdated: {IsOutdated}";

        public virtual void WritePriperties()
        {
            Console.WriteLine(Description);
        }
    }
}
