using System;

namespace HomeWork11
{
    class Program
    {
        static void Main(string[] args)
        {
            var reminderItem1 = new ReminderItem(DateTimeOffset.Parse("10:00"), "Testing");
            var reminderItem2 = new ReminderItem(alarmDate: DateTimeOffset.Parse("12:00"), alarmMessage: "Testing2");
            reminderItem1.WritePriperties();
            reminderItem2.WritePriperties();
        }
    }
}
