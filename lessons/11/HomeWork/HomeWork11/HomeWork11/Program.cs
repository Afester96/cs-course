using System;
using System.Collections.Generic;

namespace HomeWork11
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<ReminderItem>();
            list.Add(new ReminderItem(DateTimeOffset.Parse("10:00"), "Testing"));
            list.Add(new PhoneReminderItem(DateTimeOffset.Parse("10:00"), "Testing", "88005553535"));
            list.Add(new ChatReminderItem(DateTimeOffset.Parse("10:00"), "Testing", "Chattest", "Afester"));

            foreach (var item in list)
            {
                item.WritePriperties();
            }
        }
    }
}
