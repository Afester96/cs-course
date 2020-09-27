using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork11
{
    class ChatReminderItem : ReminderItem
    {
        string ChatName { get; set; }
        string AccountName { get; set; }
        public ChatReminderItem(DateTimeOffset alarmDate, string alarmMessage, string chatName, string accountName) : base(alarmDate, alarmMessage)
        {
            ChatName = chatName;
            AccountName = accountName;
        }
        public override string Description =>
            $"{base.Description}, \nChat Name: {ChatName}, Account Name: {AccountName}";
        //public override void WritePriperties() => 
        //    Console.WriteLine($"Alarm Date: {AlarmDate}, Alarm Message: {AlarmMessage}, Time To Alarm: {TimeToAlarm}," +
        //        $" Is Outdated: {IsOutdated}, Chat Name: {ChatName}, Account Name: {AccountName}");
    }
}
