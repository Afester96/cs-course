﻿using System;

namespace HomeWork11
{
    partial class ReminderItem
    {
        private DateTimeOffset _alarmDate;
        private string _alarmMessage;

        public DateTimeOffset AlarmDate { get; set; }
        public string AlarmMessage { get; set; }
        public TimeSpan TimeToAlarm => DateTimeOffset.Now - AlarmDate;
        public bool IsOutdated
        {
            get => 
                TimeToAlarm >= default(TimeSpan);
        }
    }
}
