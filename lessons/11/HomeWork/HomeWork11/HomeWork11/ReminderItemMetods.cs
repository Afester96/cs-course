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
        public void WritePriperties()
        {
            Console.WriteLine($"Alarm Date: {AlarmDate}, Alarm Message: {AlarmMessage}, Time To Alarm: {TimeToAlarm}, Is Outdated: {IsOutdated}");
        }
    }
}
