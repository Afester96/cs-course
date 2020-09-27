using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork11
{
    class PhoneReminderItem : ReminderItem
    {
        string PhoneNumber { get; set; }
        public PhoneReminderItem(DateTimeOffset alarmDate, string alarmMessage, string phoneNumber) : base(alarmDate, alarmMessage)
        {
            PhoneNumber = phoneNumber;
        }
        public override string Description =>
            $"{base.Description}, PhoneNumber: {PhoneNumber}";
        //public override void WritePriperties() =>
        //    Console.WriteLine($"Alarm Date: {AlarmDate}, Alarm Message: {AlarmMessage}, Time To Alarm: {TimeToAlarm}, " +
        //        $"Is Outdated: {IsOutdated}, PhoneNumber: {PhoneNumber}");
    }
}
