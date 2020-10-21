using System;

namespace Reminder.Sender
{
	public class ReminderNotification
	{
		public string ContactId { get; }
		public string Message { get; }
		public DateTimeOffset DateTime { get; }

		public ReminderNotification(string contactId, string message, DateTimeOffset dateTime)
		{
			ContactId = contactId;
			Message = message;
			DateTime = dateTime;
		}
	}
}